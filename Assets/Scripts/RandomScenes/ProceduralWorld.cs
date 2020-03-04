using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProceduralWorld
{

    public enum GenType { RandomBased, PerlinBased};

    public float minHeight = 0f;
    public float maxHeight = 5f;
    public int size = 50;
    public float detail = 10.0f;
    public int seed = 0;
    public GenType type;
    public float[,] heights;

    public ProceduralWorld(float minHeight, float maxHeight, int size, float detail, int seed, GenType type)
    {
        Debug.Log(message: ("Constructor of the world has been called."));
        this.minHeight = minHeight;
        this.maxHeight = maxHeight;
        this.size = size;
        this.detail = detail;
        this.seed = seed;
        this.type = type;
    }

    public void Initialize()
    {
        heights = new float[size, size];
        Generate();
    }

    public void Generate()
    {
        for (int x = 0; x < heights.GetLength(dimension: 0); x++)
        {
            for (int z = 0; z < heights.GetLength(dimension: 1); z++)
            {
                float height;

                switch (type)
                {
                    case GenType.RandomBased:
                        height = UnityEngine.Random.Range(minHeight, maxHeight); // Random genreation
                        break;

                    case GenType.PerlinBased:

                        float perlinX = (x / detail) + GameManagerRandom.instance.GetPerlinSeed();
                        float perlinZ = (z / detail) + +GameManagerRandom.instance.GetPerlinSeed();
                        height = (Mathf.PerlinNoise(perlinX, perlinZ) - minHeight) * maxHeight;
                        break;

                    default:
                        height = 0;
                        break;
                }

                heights[x, z] = height;
            }
        }

        Debug.Log(message: ("World has been generated!"));

    }
}
