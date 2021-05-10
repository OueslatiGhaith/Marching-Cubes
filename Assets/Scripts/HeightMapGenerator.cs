using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class HeightMapGenerator
{
    public static float[,,] GenerateRandom(int height, int width, int depth, bool removeBounds = true)
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
                    if (removeBounds)
                    {
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
        }
        
        return heightMap;
    }

    public static float[,,] GenerateOpenSimplex2(int height, int width, int depth, int seed, Vector3 center, float scale, bool removeBounds = true)
    {
        // create and configure Fast Noise object
        FastNoiseLite noise = new FastNoiseLite();
        noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);

        // noise seed
        noise.SetSeed(seed);

        float[,,] heightMap = new float[height, width, depth];
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    heightMap[x, y, z] = (noise.GetNoise(x * scale + center.x, y * scale + center.y, z * scale + center.z) + 1) * 10f / 2f;

                    /// triangles at the borders of the mesh can cause a problem because of the way unity renders triangles,
                    /// so I opted to remove those triangles instead.
                    if (removeBounds)
                    {
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
        }

        return heightMap;
    }

    public static float[,,] GenerateOpenSimplex2S(int height, int width, int depth, int seed, Vector3 center, float scale, bool removeBounds = true)
    {
        // create and configure Fast Noise object
        FastNoiseLite noise = new FastNoiseLite();
        noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);

        // noise seed
        noise.SetSeed(seed);

        float[,,] heightMap = new float[height, width, depth];
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    heightMap[x, y, z] = (noise.GetNoise(x * scale + center.x, y * scale + center.y, z * scale + center.z) + 1) * 10f / 2f;

                    /// triangles at the borders of the mesh can cause a problem because of the way unity renders triangles,
                    /// so I opted to remove those triangles instead.
                    if (removeBounds)
                    {
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
        }

        return heightMap;
    }

    public static float[,,] GenerateCellular(int height, int width, int depth, int seed, Vector3 center, float scale, bool removeBounds = true)
    {
        // create and configure Fast Noise object
        FastNoiseLite noise = new FastNoiseLite();
        noise.SetNoiseType(FastNoiseLite.NoiseType.Cellular);

        // noise seed
        noise.SetSeed(seed);

        float[,,] heightMap = new float[height, width, depth];
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    heightMap[x, y, z] = (noise.GetNoise(x * scale + center.x, y * scale + center.y, z * scale + center.z) + 1) * 10f / 2f;

                    /// triangles at the borders of the mesh can cause a problem because of the way unity renders triangles,
                    /// so I opted to remove those triangles instead.
                    if (removeBounds)
                    {
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
        }

        return heightMap;
    }

    public static float[,,] GeneratePerlin(int height, int width, int depth, int seed, Vector3 center, float scale, bool removeBounds = true)
    {
        // create and configure Fast Noise object
        FastNoiseLite noise = new FastNoiseLite();
        noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);

        // noise seed
        noise.SetSeed(seed);

        float[,,] heightMap = new float[height, width, depth];
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    heightMap[x, y, z] = (noise.GetNoise(x * scale + center.x, y * scale + center.y, z * scale + center.z) + 1) * 10f / 2f;

                    /// triangles at the borders of the mesh can cause a problem because of the way unity renders triangles,
                    /// so I opted to remove those triangles instead.
                    if (removeBounds)
                    {
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
        }

        return heightMap;
    }

    public static float[,,] GenerateValue(int height, int width, int depth, int seed, Vector3 center, float scale, bool removeBounds = true)
    {
        // create and configure Fast Noise object
        FastNoiseLite noise = new FastNoiseLite();
        noise.SetNoiseType(FastNoiseLite.NoiseType.Value);

        // noise seed
        noise.SetSeed(seed);

        float[,,] heightMap = new float[height, width, depth];
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    heightMap[x, y, z] = (noise.GetNoise(x * scale + center.x, y * scale + center.y, z * scale + center.z) + 1) * 10f / 2f;

                    /// triangles at the borders of the mesh can cause a problem because of the way unity renders triangles,
                    /// so I opted to remove those triangles instead.
                    if (removeBounds)
                    {
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
        }

        return heightMap;
    }

    public static float[,,] GenerateValueCubic(int height, int width, int depth, int seed, Vector3 center, float scale, bool removeBounds = true)
    {
        // create and configure Fast Noise object
        FastNoiseLite noise = new FastNoiseLite();
        noise.SetNoiseType(FastNoiseLite.NoiseType.ValueCubic);

        // noise seed
        noise.SetSeed(seed);

        float[,,] heightMap = new float[height, width, depth];
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    heightMap[x, y, z] = (noise.GetNoise(x * scale + center.x, y * scale + center.y, z * scale + center.z) + 1) * 10f / 2f;

                    /// triangles at the borders of the mesh can cause a problem because of the way unity renders triangles,
                    /// so I opted to remove those triangles instead.
                    if (removeBounds)
                    {
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
        }

        return heightMap;
    }
}
