using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Ionic.Zlib;

namespace MinecraftRetroBiomes
{
    public struct Coord
    {
        public int x;
        public int z;

        public Coord(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        public Coord(Coord c)
        {
            this.x = c.x;
            this.z = c.z;
        }

        public void Add(int dx, int dz)
        {
            x += dx;
            z += dz;
        }

        public void ChunktoAbsolute()
        {
            x *= 16;
            z *= 16;
        }

        public void AbsolutetoChunk()
        {
            x = (int)Math.Floor(((double)x) / 16.0);
            z = (int)Math.Floor(((double)z) / 16.0);
        }

        public void RegiontoAbsolute()
        {
            x = x * 16 * 32;
            z = z * 16 * 32;
        }

        public void AbsolutetoRegion()
        {
            x = (int)Math.Floor(((double)x) / 32.0 / 16.0);
            z = (int)Math.Floor(((double)z) / 32.0 / 16.0);
        }

        public void RegiontoChunk()
        {
            x = x * 32;
            z = z * 32;
        }

        public void ChunktoRegion()
        {
            x = (int)Math.Floor(((double)x) / 32.0);
            z = (int)Math.Floor(((double)z) / 32.0);
        }
    }

    public class Chunk
    {
        public TAG_Compound Root;
        public Coord Coords = new Coord();
        public Int32 Timestamp;

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            DateTime time = new DateTime(1970, 1, 1).AddSeconds(Timestamp);

            sb.AppendFormat("Chunk [{0}, {1}] {2:M/d/yyyy h:mm:ss tt}\r\n{{\r\n", Coords.x, Coords.z, time);
            if(Root != null)
                sb.Append(Root.ToString());
            sb.Append("}\r\n");
            return sb.ToString();
        }
    }

    public class RegionFile
    {
        public List<Chunk> Chunks;
        public Coord Coords = new Coord();
        private ProgressUpdateDelegate2 progressUpdate2 = null;

        public RegionFile()
        {
            Chunks = new List<Chunk>();
        }

        public RegionFile(String path)
        {
            Read(path);
        }

        public RegionFile(String path, ProgressUpdateDelegate2 progressUpdate2)
        {
            this.progressUpdate2 = progressUpdate2;
            Read(path);
        }

        //http://www.minecraftwiki.net/wiki/Region_file_format
        public void Read(String path)
        {
            Chunks = new List<Chunk>();
            Match m = Regex.Match(path, @"r\.(-?\d+)\.(-?\d+)\.mca");
            Coords.x = int.Parse(m.Groups[1].Value);
            Coords.z = int.Parse(m.Groups[2].Value);
            
            byte[] header = new byte[8192];

            using (BinaryReader file = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                file.Read(header, 0, 8192);

                if (progressUpdate2 != null)
                    progressUpdate2.Invoke(0, 1024, "Reading and decompressing chunks");
                int count = 0;
                for (int chunkZ = 0; chunkZ < 32; chunkZ++)
                {
                    for (int chunkX = 0; chunkX < 32; chunkX++)
                    {
                        Chunk c = new Chunk();
                        c.Coords.x = Coords.x;
                        c.Coords.z = Coords.z;
                        c.Coords.RegiontoChunk();
                        c.Coords.Add(chunkX, chunkZ);

                        int i = 4 * (chunkX + chunkZ * 32);

                        byte[] temp = new byte[4];
                        temp[0] = 0;
                        Array.Copy(header, i, temp, 1, 3);
                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(temp);
                        long offset = ((long)BitConverter.ToInt32(temp, 0)) * 4096;
                        int length = header[i + 3] * 4096;

                        temp = new byte[4];
                        Array.Copy(header, i + 4096, temp, 0, 4);
                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(temp);
                        c.Timestamp = BitConverter.ToInt32(temp, 0);

                        if (offset == 0 && length == 0)
                        {
                            Chunks.Add(c);
                            continue;
                        }

                        file.BaseStream.Seek(offset, SeekOrigin.Begin);

                        temp= new byte[4];
                        file.Read(temp, 0, 4);
                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(temp);
                        int exactLength = BitConverter.ToInt32(temp, 0);

                        int compressionType = file.ReadByte();
                        if (compressionType == 1) //GZip
                        {
                            byte[] data = new byte[exactLength - 1];
                            file.Read(data, 0, exactLength - 1);

                            GZipStream decompress = new GZipStream(new MemoryStream(data), CompressionMode.Decompress);
                            MemoryStream mem = new MemoryStream();
                            decompress.CopyTo(mem);
                            mem.Seek(0, SeekOrigin.Begin);
                            c.Root = new TAG_Compound();
                        }
                        else if (compressionType == 2) //Zlib
                        {
                            byte[] data = new byte[exactLength - 1];
                            file.Read(data, 0, exactLength - 1);

                            ZlibStream decompress = new ZlibStream(new MemoryStream(data), CompressionMode.Decompress);
                            MemoryStream mem = new MemoryStream();
                            decompress.CopyTo(mem);
                            mem.Seek(0, SeekOrigin.Begin);
                            c.Root = new TAG_Compound(mem);
                        }
                        else
                        {
                            throw new Exception("Unrecognized compression type");
                        }
                        
                        Chunks.Add(c);
                        if (progressUpdate2 != null)
                            progressUpdate2.Invoke(++count, 1024, null);
                    }
                }

                file.Close();
            }
        }

        public void Write(String path)
        {
            byte[] header = new byte[8192];
            Array.Clear(header, 0, 8192);

            Int32 sectorOffset = 2;
            if (progressUpdate2 != null)
                progressUpdate2.Invoke(0, 1024, "Compressing and writing chunks");
            int count = 0;
            using (BinaryWriter file = new BinaryWriter(File.Exists(path) ? File.Open(path, FileMode.Truncate) : File.Open(path, FileMode.Create)))
            {
                file.Write(header, 0, 8192);

                foreach (Chunk c in Chunks)
                {
                    int chunkX = c.Coords.x % 32;
                    if (chunkX < 0)
                        chunkX += 32;
                    int chunkZ = c.Coords.z % 32;
                    if (chunkZ < 0)
                        chunkZ += 32;

                    int i = 4 * (chunkX + chunkZ * 32);

                    byte[] temp = BitConverter.GetBytes(c.Timestamp);
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(temp);
                    Array.Copy(temp, 0, header, i + 4096, 4);

                    if (c.Root == null)
                    {
                        Array.Clear(temp, 0, 4);
                        Array.Copy(temp, 0, header, i, 4);
                        continue;
                    }

                    temp = BitConverter.GetBytes(sectorOffset);
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(temp);
                    Array.Copy(temp, 1, header, i, 3);

                    MemoryStream mem = new MemoryStream();
                    c.Root.Write(mem);
                    temp = mem.ToArray();
                    byte[] data = ZlibStream.CompressBuffer(temp);

                    temp = BitConverter.GetBytes(data.Length + 1);
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(temp);

                    file.Write(temp, 0, 4);
                    file.Write((byte)2);//Zlib
                    file.Write(data, 0, data.Length);

                    byte[] padding = new byte[(4096 - ((data.Length + 5) % 4096))];
                    Array.Clear(padding, 0, padding.Length);
                    file.Write(padding);

                    header[i + 3] = (byte)((data.Length + 5) / 4096 + 1);
                    sectorOffset += (data.Length + 5) / 4096 + 1;

                    if (progressUpdate2 != null)
                        progressUpdate2.Invoke(++count, 1024, null);
                }

                if (progressUpdate2 != null)
                    progressUpdate2.Invoke(1024, 1024, null);

                file.Seek(0, SeekOrigin.Begin);
                file.Write(header, 0, 8192);
                file.Flush();
                file.Close();
            }

        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Region [{0}, {1}]\r\n{{\r\n", Coords.x, Coords.z);
            foreach (Chunk c in Chunks)
                sb.Append(c.ToString());
            sb.Append("}\r\n");
            return sb.ToString();
        }
    }
}
