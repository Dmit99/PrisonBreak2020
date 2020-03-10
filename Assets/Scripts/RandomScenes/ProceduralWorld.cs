using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProceduralWorld
{

    public enum GenType { RandomBased, PerlinBased, sinBased};

    public List<GameObject> rockPrefab;
    [SerializeField] private float minHeight = 0f;
    [SerializeField] private float maxHeight = 0.001f;
    [SerializeField] private int size = 50;
    [SerializeField] private float detail = 2.0f;
    [SerializeField] private float rockProbability;
    [SerializeField] private int seed = 0;
    [SerializeField] private GenType type;
    public float[,] heights;
    public List<Vector3Int> rocks;

    public int Size
    {
        get { return size; }
        set { size = value; Initialize(); }
    }

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

                        float perlinX = GameManagerRandom.instance.GetPerlinSeed() + x / (float)GameManagerRandom.instance.world.size * detail;
                        float perlinZ= GameManagerRandom.instance.GetPerlinSeed() + z / (float)GameManagerRandom.instance.world.size * detail;
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

                /// Instanciating random rocks.
                float rockRand = UnityEngine.Random.value;

                if(rockRand < rockProbability)
                {
                    int t = UnityEngine.Random.Range(0, rockPrefab.Count);
                    Vector3Int rock = new Vector3Int(x: x, y: z, z: t);
                    rocks.Add(rock);
                }
            }
        }

        Debug.Log(message: ("World has been generated!"));

    }
}
