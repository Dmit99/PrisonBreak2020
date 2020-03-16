using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProceduralWorld
{

    public enum GenType { RandomBased, PerlinBased, sinBased, Island};

    public List<GameObject> assetsPfb;
    [SerializeField] private float minHeight = 0f;
    [SerializeField] private float maxHeight = 0.001f;
    [SerializeField] private int size = 50;
    [SerializeField] private float detail = 2.0f;
    [SerializeField] private float assetProbability;
    [SerializeField] private int seed = 0;
    [SerializeField] private GenType type;
    public float[,] heights;
    public List<Vector3Int> assets;
    public List<Texture> terrainTextures;

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
                        float perlinZ = GameManagerRandom.instance.GetPerlinSeed() + z / (float)GameManagerRandom.instance.world.size * detail;
                        height = (Mathf.PerlinNoise(perlinX, perlinZ) - minHeight) * maxHeight;
                        break;

                    case GenType.sinBased:
                        float sinX = Mathf.Sin(x / detail);
                        float sinZ = Mathf.Cos(z / detail);
                        float combined = sinX + sinZ;
                        height = (combined - minHeight) * maxHeight;
                        break;

                    case GenType.Island:
                        Vector2 midpoint;
                        midpoint = new Vector2(x: GameManagerRandom.instance.GetPerlinSeed() + x / (float)GameManagerRandom.instance.world.size * detail, y: GameManagerRandom.instance.GetPerlinSeed() + z / (float)GameManagerRandom.instance.world.size * detail);

                        float distance = Vector2.Distance(midpoint, new Vector2(x, z));
                        float pSeed = GameManagerRandom.instance.GetPerlinSeed();
                        float perlinXisland = (float)x / detail + pSeed;
                        float perlinZisland = (float)z / detail + pSeed;

                        height = (Mathf.PerlinNoise(perlinXisland, perlinZisland) - minHeight) * maxHeight + (Mathf.Cos(distance / detail) * maxHeight);

                        if (height > maxHeight)
                        {
                            height += UnityEngine.Random.Range(minHeight, maxHeight) / distance;
                        }
                        break;

                    default:
                        height = 0;
                        break;
                }

                heights[x, z] = height;

                /// Instanciating random rocks.
                float assetRand = UnityEngine.Random.value;

                if (assetRand < assetProbability * (maxHeight / height)) /// <- heighest point of the terrain, less assets.
                {
                    int t = UnityEngine.Random.Range(0, assetsPfb.Count);
                    Vector3Int asset = new Vector3Int(x: x, y: z, z: t);
                    assets.Add(asset);
                }
            }
        }
    }
}
