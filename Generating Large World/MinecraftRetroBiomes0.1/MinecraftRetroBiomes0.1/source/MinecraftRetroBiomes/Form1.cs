using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Minecraft;

namespace MinecraftRetroBiomes
{
    public partial class Form1 : Form
    {
        private MCWorld world;
        private Thread worker = null;
        private String lastPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.minecraft\saves";
        private const String separator = "----------------------------------------";

        public Form1()
        {
            InitializeComponent();
            txtOutput.Text = @"Hello! And welcome. This program takes a Minecraft world, already in the new (circa February 2012) ""Anvil"" format (you must have already converted it using 12w07a or later) and fills out the ""Biomes"" data tag for each and every chunk based on what biomes would have been at whichever coordinates if this were still Minecraft Beta 1.7 on back.

Since the dramatically different terrain generation introduced in Beta 1.8, biomes are no longer where they used to be for worlds created in Beta 1.7 or prior. Grass isn't the same color in the same places, and snow and rain occur independent of where deserts, tundra, and taiga used to begin and end.

This program is designed to be used with worlds where a majority or all of the terrain was generated before Beta 1.8 that you still want to use with the latest version of Minecraft, but with the biomes where they were prior to September 2011.

EXTREMELY IMPORTANT NOTES:
# You really, really, REALLY need to make a backup copy of your world before you run this program on it, just in case. Seriously, do it. Do it now.

# If your world contains chunks generated with Beta 1.8 or later, this program will still work, however it will still blindly force biomes as they were located in Beta 1.7 over every single chunk, regardless of when that chunk was generated. If that isn't what you want, I'm sorry but all I can suggest is digging into the source code yourself or waiting for someone to develop a better tool.

# Not all the old biome types exist in the current version of Minecraft. For instance, this program takes all locations that in 1.7 on back would have been ""Shrubland"", ""Seasonal Forest"", or ""Forest"" and tags them as being in a ""Forest"" biome because the first two biomes no longer exist Minecraft. For more information, see ""MCWorld.cs"", the function ""GoGoGadget()"".

# There appears to be a bug in at least 12w07a where weather properly recognizes the specified biome in every way except for the rain/snow falling through the air effect. So, the rain sound or silence, droplets splashing off the ground or not, and ice or snow accumulating or not all work properly. It just may look like it still is raining or snowing or not where it shouldn't until Jeb hopefully fixed this bug.
";
            txtOutput.Select(0, 0);
        }
        private void btnCheck_Click(object sender, EventArgs e)
        {
            btnDoIt.Enabled = false;
            btnCheck.Enabled = false;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = lastPath;
            dialog.Filter = "(*.dat)|*.dat";
            dialog.RestoreDirectory = false;

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                btnCheck.Enabled = true;
                return;
            }

            lastPath = dialog.FileName;
            world = new MCWorld(lastPath, ProgressUpdate1, ProgressUpdate2, ThreadDone);

            int regionCount = world.Check();

            StringBuilder sb = new StringBuilder();
            if (regionCount <= 0)
            {
                sb.AppendFormat("No *.mca region files found for world at:\r\n{0}\r\nA world must first be converted to the \"Anvil\" file format using Minecraft 12w07a or later.\r\n", lastPath);
            }
            else
            {
                sb.AppendFormat("Found {0} *.mca region files for world at:\r\n{1}\r\nWorld seed: {2}\r\nIf you have already made a backup copy of your world, click \"Do it!\" when ready. Remember, this will most likely take a while.\r\n", regionCount, lastPath, world.Seed);
                btnDoIt.Enabled = true;
            }
            sb.AppendFormat("{0}\r\n{1}", separator, txtOutput.Text);
            txtOutput.Text = sb.ToString();
            btnCheck.Enabled = true;
        }

        private void btnDoIt_Click(object sender, EventArgs e)
        {
            btnDoIt.Enabled = false;
            btnCheck.Enabled = false;
            progress1.Value = 0;
            progress2.Value = 0;
            lblCurrentOp.Text = "";

            worker = new Thread(new ThreadStart(world.GoGoGadget));
            worker.Start();
        }

        public void ThreadDone(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler(ThreadDone), sender, e);
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Done!\r\n{0}\r\n{1}", separator, txtOutput.Text);
            txtOutput.Text = sb.ToString();
            lblCurrentOp.Text = "";
            btnCheck.Enabled = true;
            btnDoIt.Enabled = true;
            worker = null;
        }

        private void Indent()
        {
            int indent = 0;
            StringBuilder sb = new StringBuilder();
            foreach (String line in txtOutput.Lines)
            {
                if (line.Length > 0 && line[0] == '{')
                {
                    sb.Append(new String('\t', indent)).AppendLine(line);
                    indent++;
                }
                else if (line.Length > 0 && line[0] == '}')
                {
                    indent--;
                    sb.Append(new String('\t', indent)).AppendLine(line);
                }
                else
                    sb.Append(new String('\t', indent)).AppendLine(line);
            }
            txtOutput.Text = sb.ToString();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            txtOutput.Width = Width - 35;
            txtOutput.Height = Height - 134;
            progress1.Width = Width - 193;
            progress2.Width = Width - 193;
        }

        //http://schotime.net/blog/index.php/2008/03/12/select-all-ctrla-for-textbox/
        private void txtOutput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == System.Windows.Forms.Keys.A))
            {
                txtOutput.SelectAll();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else
                base.OnKeyDown(e);
        }

        public void ProgressUpdate1(int value, int max)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ProgressUpdateDelegate1(ProgressUpdate1), value, max);
                return;
            }
            progress1.Maximum = max;
            progress1.Value = value;
            progress1.Refresh();
        }

        public void ProgressUpdate2(int value, int max, String label)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ProgressUpdateDelegate2(ProgressUpdate2), value, max, label);
                return;
            }

            if (label != null)
            {
                lblCurrentOp.Text = label;
                lblCurrentOp.Refresh();
            }
            progress2.Maximum = max;
            progress2.Value = value;
            progress2.Refresh();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (worker != null)
            {
                if (DialogResult.OK != MessageBox.Show("This program is still operating on your Minecraft world, are you sure you want to stop in the middle?", "Still working, dude", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    e.Cancel = true;
                else
                {
                    if(worker != null)
                        worker.Abort();
                }
            }
        }
    }
}
