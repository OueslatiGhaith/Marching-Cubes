using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class GridMaker : MonoBehaviour
{
    [Range(1,100)] public int height = 10;
    [Range(1, 100)] public int width = 10;
    [Range(1, 100)] public int depth = 10;
    [Range(0, 1)] public float surfaceLevel = 0.5f;
    float[,,] heightMap;

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;

    private void OnValidate()
    {
        heightMap = HeightMapGenerator.GenerateHeightMap(height, width, depth);
        Debug.Log(heightMap.Length);
        ConstructMesh();
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                for (int z = 0; z < depth; z++)
                {                                     
                    if (heightMap[x, y, z] < surfaceLevel) 
                    {
                        Gizmos.color = Color.gray;
                        Gizmos.DrawCube(new Vector3(x, y, z), new Vector3(0.01f, 0.01f, 0.01f));
                    } else
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawCube(new Vector3(x, y, z), new Vector3(0.1f, 0.1f, 0.1f));
                    }
                }
            }
        }
    }

    private void ConstructMesh()
    {
        if (meshFilter == null || meshRenderer == null) 
        { 
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
        }
        meshFilter.sharedMesh.Clear();
        meshFilter.sharedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        int triCount = 0;
        // performs the marching cubes algorithm on a single cube each iteration
        for (int x = 0; x < height - 1; x++)
        {
            for (int y = 0; y < width - 1; y++)
            {
                for (int z = 0; z < depth - 1; z++)
                {
                    // gets a cube
                    float[] values = new float[8];

                    values[0] = heightMap[x, y, z];
                    values[1] = heightMap[x + 1, y, z];
                    values[2] = heightMap[x + 1, y, z + 1];
                    values[3] = heightMap[x, y, z + 1];

                    values[4] = heightMap[x, y + 1, z];
                    values[5] = heightMap[x + 1, y + 1, z];
                    values[6] = heightMap[x + 1, y + 1, z +1];
                    values[7] = heightMap[x, y + 1, z + 1];

                    // gets the index of this particular cube in the triangulation table
                    int cubeIndex = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        if(values[i] < surfaceLevel)
                        {
                            cubeIndex |= 1 << i;
                        }
                    }

                    // lookup the triangulation for the current cubeIndex
                    // each entry in the index is an edge
                    int[] triangulation = TriangluationTable.GetConfiguration(cubeIndex);
                    foreach (int edgeIndex in triangulation)
                    {
                        // lookup the positions of the corner points making up the current edge
                        Vector3 cornerA = new Vector3();
                        Vector3 cornerB = new Vector3();
                        bool isVertex = true;
                        switch(edgeIndex)
                        {
                            case 0:
                                cornerA = new Vector3(x, y, z);
                                cornerB = new Vector3(x + 1, y, z);
                                break;
                            case 1:
                                cornerA = new Vector3(x + 1, y, z);
                                cornerB = new Vector3(x + 1, y, z + 1);
                                break;
                            case 2:
                                cornerA = new Vector3(x + 1, y, z + 1);
                                cornerB = new Vector3(x, y, z + 1);
                                break;
                            case 3:
                                cornerA = new Vector3(x, y, z + 1);
                                cornerB = new Vector3(x, y, z);
                                break;
                            case 4:
                                cornerA = new Vector3(x, y + 1, z);
                                cornerB = new Vector3(x + 1, y + 1, z);
                                break;
                            case 5:
                                cornerA = new Vector3(x + 1, y + 1, z);
                                cornerB = new Vector3(x + 1, y + 1, z + 1);
                                break;
                            case 6:
                                cornerA = new Vector3(x + 1, y + 1, z + 1);
                                cornerB = new Vector3(x, y + 1, z + 1);
                                break;
                            case 7:
                                cornerA = new Vector3(x, y + 1, z + 1);
                                cornerB = new Vector3(x, y + 1, z);
                                break;
                            case 8:
                                cornerA = new Vector3(x, y, z);
                                cornerB = new Vector3(x, y + 1, z);
                                break;
                            case 9:
                                cornerA = new Vector3(x + 1, y, z);
                                cornerB = new Vector3(x + 1, y + 1, z);
                                break;
                            case 10:
                                cornerA = new Vector3(x + 1, y, z + 1);
                                cornerB = new Vector3(x + 1, y + 1, z + 1);
                                break;
                            case 11:
                                cornerA = new Vector3(x, y, z + 1);
                                cornerB = new Vector3(x, y + 1, z + 1);
                                break;
                            default:
                                isVertex = false;
                                break;
                        }

                        // the vertex position is the midpoint of the edge
                        if (isVertex) 
                        {
                            Vector3 vertexPos = (cornerA + cornerB) / 2;
                            vertices.Add(vertexPos);
                            triangles.Add(triCount);
                            triCount++;
                        }
                    }
                }
            }
        }

        // set the mesh filter and mesh renderer
        meshFilter.sharedMesh.vertices = vertices.ToArray();
        meshFilter.sharedMesh.triangles = triangles.ToArray();
        meshFilter.sharedMesh.RecalculateNormals();
    }
}
