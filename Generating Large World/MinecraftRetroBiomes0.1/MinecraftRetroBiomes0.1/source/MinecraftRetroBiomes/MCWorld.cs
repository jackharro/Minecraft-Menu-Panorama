using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Minecraft;
using Ionic.Zlib;

namespace MinecraftRetroBiomes
{
    public enum NewBiome : byte
    {
        Ocean = 0,
        Plains = 1,
        Desert = 2,
        ExtremeHills = 3,
        Forest = 4,
        Taiga = 5,
        Swampland = 6,
        River = 7,
        Hell = 8,
        Sky = 9,
        FrozenOcean = 10,
        FrozenRiver = 11,
        IcePlains = 12,
        IceMountains = 13,
        MushroomIsland = 14,
        MushroomIslandShore = 15,
        Beach = 16,
        DesertHills = 17,
        ForestHills = 18,
        TaigaHills = 19,
        ExtremeHillsEdge = 20,
        Jungle = 21,
        JungleHills = 22
    }

    public delegate void ProgressUpdateDelegate1(int value, int max);
    public delegate void ProgressUpdateDelegate2(int value, int max, String label);

    public class MCWorld
    {
        public long Seed;
        public String RegionDir;
        private ProgressUpdateDelegate1 progressUpdate1;
        private ProgressUpdateDelegate2 progressUpdate2;
        private event EventHandler threadDone;

        public MCWorld(String path, ProgressUpdateDelegate1 progressUpdate1, ProgressUpdateDelegate2 progressUpdate2, EventHandler threadDone)
        {
            this.progressUpdate1 = progressUpdate1;
            this.progressUpdate2 = progressUpdate2;
            this.threadDone = threadDone;
            TAG_Compound data;

            using (FileStream level = new FileStream(path, FileMode.Open))
            {
                using (GZipStream decompress = new GZipStream(level, CompressionMode.Decompress))
                {
                    MemoryStream mem = new MemoryStream();
                    decompress.CopyTo(mem);
                    mem.Seek(0, SeekOrigin.Begin);
                    data = new TAG_Compound(mem);
                    data = (TAG_Compound)data[""];
                }
            }

            Seed = ((TAG_Long)((TAG_Compound)data["Data"])["RandomSeed"]).Payload;
            RegionDir = String.Format("{0}{1}region", Path.GetDirectoryName(path), Path.DirectorySeparatorChar);
        }

        public int Check()
        {
            return Directory.GetFiles(RegionDir, "*.mca", SearchOption.TopDirectoryOnly).Length;
        }

        public void GoGoGadget()
        {
            WorldChunkManager world = new WorldChunkManager(Seed);
            String[] files = Directory.GetFiles(RegionDir, "*.mca", SearchOption.TopDirectoryOnly);

            progressUpdate1.Invoke(0, files.Length);
            int count = 0;
            foreach (String file in files)
            {
                RegionFile region = new RegionFile(file, progressUpdate2);
                progressUpdate2.Invoke(0, region.Chunks.Count, "Calculating biomes, udpating chunks");
                int chunkCount = 0;
                foreach (Chunk c in region.Chunks)
                {
                    if (c.Root == null)
                        continue;
                    TAG_Byte_Array biomes = (TAG_Byte_Array)((TAG_Compound)((TAG_Compound)c.Root[""])["Level"])["Biomes"];
                    for (int z = 0; z < 16; z++)
                    {
                        for (int x = 0; x < 16; x++)
                        {
                            Coord coords = new Coord(c.Coords);
                            coords.ChunktoAbsolute();
                            coords.Add(x, z);

                            Biome oldBiome = BiomeGenBase.getBiome(((float)world.getTemperature(coords.x, coords.z)), ((float)world.getHumidity(coords.x, coords.z)));
                            NewBiome newBiome = (NewBiome)0;

                            switch (oldBiome)
                            {
                                case Biome.Taiga:
                                    newBiome = NewBiome.Taiga;
                                    break;
                                case Biome.Tundra:
                                    newBiome = NewBiome.IcePlains;
                                    break;
                                case Biome.Savanna:
                                case Biome.Plains:
                                    newBiome = NewBiome.Plains;
                                    break;
                                case Biome.Desert:
                                    newBiome = NewBiome.Desert;
                                    break;
                                case Biome.Swampland:
                                case Biome.Shrubland:
                                case Biome.Forest:
                                case Biome.SeasonalForest:
                                    newBiome = NewBiome.Forest;
                                    break;
                                case Biome.Rainforest:
                                    newBiome = NewBiome.Jungle;
                                    break;
                                default:
                                    throw new Exception("crap");
                            }
                            biomes.Payload[x + z * 16] = (byte)newBiome;
                        }
                    }

                    progressUpdate2.Invoke(++chunkCount, region.Chunks.Count, null);
                }

                region.Write(file);
                progressUpdate1.Invoke(++count, files.Length);
            }

            threadDone.Invoke(this, null);
        }
    }
}
