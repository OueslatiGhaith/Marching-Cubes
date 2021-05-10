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

                    /// triangles at the borders of the mesh can cause a problem because of the way unity renders triangles,
                    /// so I opted to remove those triangles instead.
                    if (x == 0 || x == height - 1 || y == 0 || y == width - 1 || z == 0 || z == depth - 1)
                    {
                        if (height != 2 && width != 2 && depth != 2)
                        {
                            heightMap[x, y, z] = 0;
                        }
                    }
                }
            }
        }

        

        return heightMap;
    }
}
