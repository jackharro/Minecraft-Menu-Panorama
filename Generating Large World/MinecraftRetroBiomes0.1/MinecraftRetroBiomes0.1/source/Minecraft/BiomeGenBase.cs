using System;

namespace Minecraft
{
    public enum Biome
    {
        Taiga,
        Tundra,
        Savanna,
        Desert,
        Swampland,
        Shrubland,
        Forest,
        Plains,
        SeasonalForest,
        Rainforest
    }

    public class BiomeGenBase
    {
        public static Biome getBiome(float temp, float humid)
        {
            humid *= temp;
            if (temp < 0.1F)
            {
                return Biome.Tundra;
            }
            if (humid < 0.2F)
            {
                if (temp < 0.5F)
                {
                    return Biome.Tundra;
                }
                if (temp < 0.95F)
                {
                    return Biome.Savanna;
                }
                else
                {
                    return Biome.Desert;
                }
            }
            if (humid > 0.5F && temp < 0.7F)
            {
                return Biome.Swampland;
            }
            if (temp < 0.5F)
            {
                return Biome.Taiga;
            }
            if (temp < 0.97F)
            {
                if (humid < 0.35F)
                {
                    return Biome.Shrubland;
                }
                else
                {
                    return Biome.Forest;
                }
            }
            if (humid < 0.45F)
            {
                return Biome.Plains;
            }
            if (humid < 0.9F)
            {
                return Biome.SeasonalForest;
            }
            else
            {
                return Biome.Rainforest;
            }
        }
    }

}
