using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProceduralWorld
{

    public enum GenType { RandomBased, PerlinBased, sinBased};

    [SerializeField] private float minHeight = 0f;
    [SerializeField] private float maxHeight = 5f;
    [SerializeField] private int size = 50;
    [SerializeField] private float detail = 10.0f;
    [SerializeField] private GenType type;
    private int seed = 0;
    public float[,] heights;

    //public float MinHeight
    //{
    //    get { return minHeight; }
    //    set { minHeight = value; Initialize(); }
    //}

    //public float MaxHeight
    //{
    //    get { return maxHeight; }
    //    set { maxHeight = value; Initialize(); }
    //}

    //public int Size
    //{
    //    get { return size; }
    //    set { size = value; Initialize(); }
    //}

    //public float Detail
    //{
    //    get { return detail; }
    //    set { detail = value; Initialize(); }
    //}

    //public int Seed
    //{
    //    get { return seed; }
    //    set { seed = value; Initialize(); }
    //}

    //public GenType TypeGen
    //{
    //    get { return type; }
    //    set { type = value; Initialize(); }
    //}

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
        GameManagerRandom.instance.regenerate.AddListener(Regenerate);
        Regenerate();
    }

    public void Regenerate()
    {
        heights = new float[size, size];
        GameManagerRandom.instance.SetSeed(seed: seed);
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

                    case GenType.sinBased:
                        float sinX = Mathf.Sin(x / detail);
                        float sinZ = Mathf.Cos(z/detail);
                        float combined = sinX + sinZ;
                        height = (combined - minHeight) * maxHeight;
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
