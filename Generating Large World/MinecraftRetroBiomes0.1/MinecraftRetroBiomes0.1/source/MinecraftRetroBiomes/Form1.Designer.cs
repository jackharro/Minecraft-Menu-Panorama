namespace MinecraftRetroBiomes
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.progress1 = new System.Windows.Forms.ProgressBar();
            this.btnDoIt = new System.Windows.Forms.Button();
            this.progress2 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCurrentOp = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(9, 85);
            this.txtOutput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(719, 405);
            this.txtOutput.TabIndex = 0;
            this.txtOutput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOutput_KeyDown);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(9, 10);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(70, 23);
            this.btnCheck.TabIndex = 1;
            this.btnCheck.Text = "Check";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // progress1
            // 
            this.progress1.Location = new System.Drawing.Point(166, 10);
            this.progress1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.progress1.Name = "progress1";
            this.progress1.Size = new System.Drawing.Size(561, 23);
            this.progress1.TabIndex = 2;
            // 
            // btnDoIt
            // 
            this.btnDoIt.Enabled = false;
            this.btnDoIt.Location = new System.Drawing.Point(10, 38);
            this.btnDoIt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDoIt.Name = "btnDoIt";
            this.btnDoIt.Size = new System.Drawing.Size(70, 23);
            this.btnDoIt.TabIndex = 3;
            this.btnDoIt.Text = "Do it!";
            this.btnDoIt.UseVisualStyleBackColor = true;
            this.btnDoIt.Click += new System.EventHandler(this.btnDoIt_Click);
            // 
            // progress2
            // 
            this.progress2.Location = new System.Drawing.Point(166, 38);
            this.progress2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.progress2.Name = "progress2";
            this.progress2.Size = new System.Drawing.Size(561, 23);
            this.progress2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "All regions:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 43);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Current region:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(85, 67);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Current op:";
            // 
            // lblCurrentOp
            // 
            this.lblCurrentOp.AutoSize = true;
            this.lblCurrentOp.Location = new System.Drawing.Point(166, 67);
            this.lblCurrentOp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCurrentOp.Name = "lblCurrentOp";
            this.lblCurrentOp.Size = new System.Drawing.Size(0, 13);
            this.lblCurrentOp.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 501);
            this.Controls.Add(this.lblCurrentOp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progress2);
            this.Controls.Add(this.btnDoIt);
            this.Controls.Add(this.progress1);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.txtOutput);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Minecraft Retro Biomes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.ProgressBar progress1;
        private System.Windows.Forms.Button btnDoIt;
        private System.Windows.Forms.ProgressBar progress2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCurrentOp;
    }
}

