using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class HeightMapGenerator
{
    public static float[,,] GenerateHeightMap(int height, int width, int depth)
    {
        float[,,] heightMap = new float[height, width, depth];

        System.Random rng = new System.Random();
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    heightMap[x, y, z] = rng.Next(0, 100) / 100f;
                }
            }
        }

        return heightMap;
    }
}
