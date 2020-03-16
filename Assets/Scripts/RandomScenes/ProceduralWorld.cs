using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; /// Used for Sum of array. Vector3.normalize() is incorrect.

[Serializable]
public class ProceduralWorld
{
    public enum GenType { RandomBased, PerlinBased, sinBased, Island};

    [Header("Global.")]
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
    private int enoughParts = 0;
    private bool skipAPart = false;

    public int Size
    {
        get { return size; }
        set { size = value; Initialize(); }
    }

    public ProceduralWorld(float minHeight, float maxHeight, int size, float detail, GenType type)
    {
        Debug.Log(message: ("Constructor of the world has been called."));
        this.minHeight = minHeight;
        this.maxHeight = maxHeight;
        this.size = size;
        this.detail = detail;
        this.type = type;
    }


    public void Initialize()
    {
        GameManagerRandom.instance.regenerate.AddListener(Regenerate);
        seed = PlayerPrefs.GetInt("SeedNumber");
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
                    if (t == 10 || t == 11 || t == 12)
                    {
                        if (enoughParts != 10)
                        {
                            if (!skipAPart)
                            {
                                int randomPositionX = UnityEngine.Random.Range(0, 256);
                                int randomPositionZ = UnityEngine.Random.Range(0, (int)maxHeight);
                                skipAPart = !skipAPart;
                                Vector3Int asset = new Vector3Int(x: x + randomPositionX, y: z+ randomPositionZ, z: t);
                                assets.Add(asset);
                                enoughParts++;
                            }
                            else
                            {
                                skipAPart = !skipAPart;
                            }
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                        Vector3Int asset = new Vector3Int(x: x, y: z, z: t);
                        assets.Add(asset);
                    }
                }
            }
        }
        /// After world made place textures.
        LoadingTextures();
    }

    public void LoadingTextures()
    {
        // Get the attached terrain component
        Terrain terrain = Terrain.activeTerrain.GetComponent<Terrain>();

        // Get a reference to the terrain data
        TerrainData terrainData = terrain.terrainData;

        // Splatmap data is stored internally as a 3d array of floats, so declare a new empty array ready for your custom splatmap data:
        float[,,] splatmapData = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];

        for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < terrainData.alphamapWidth; x++)
            {
                // Normalise x/y coordinates to range 0-1 
                float y_01 = (float)y / (float)terrainData.alphamapHeight;
                float x_01 = (float)x / (float)terrainData.alphamapWidth;

                // Sample the height at this location (note GetHeight expects int coordinates corresponding to locations in the heightmap array)
                float height = terrainData.GetHeight(Mathf.RoundToInt(y_01 * terrainData.heightmapHeight), Mathf.RoundToInt(x_01 * terrainData.heightmapWidth));

                // Calculate the normal of the terrain (note this is in normalised coordinates relative to the overall terrain dimensions)
                Vector3 normal = terrainData.GetInterpolatedNormal(y_01, x_01);

                // Calculate the steepness of the terrain
                float steepness = terrainData.GetSteepness(y_01, x_01);

                // Setup an array to record the mix of texture weights at this point
                float[] splatWeights = new float[terrainData.alphamapLayers];

                // CHANGE THE RULES BELOW TO SET THE WEIGHTS OF EACH TEXTURE ON WHATEVER RULES YOU WANT

                // Texture[0] has constant influence
                splatWeights[0] = 0.5f;

                // Texture[1] is stronger at lower altitudes
                splatWeights[1] = Mathf.Clamp01((terrainData.heightmapHeight - height));

                // Texture[2] stronger on flatter terrain
                // Note "steepness" is unbounded, so we "normalise" it by dividing by the extent of heightmap height and scale factor
                // Subtract result from 1.0 to give greater weighting to flat surfaces
                splatWeights[2] = 1.0f - Mathf.Clamp01(steepness * steepness / (terrainData.heightmapHeight / 5.0f));

                // Texture[3] increases with height but only on surfaces facing positive Z axis 
                splatWeights[3] = height * Mathf.Clamp01(normal.z);

                // Sum of all textures weights must add to 1, so calculate normalization factor from sum of weights
                float z = splatWeights.Sum();

                // Loop through each terrain texture
                for (int i = 0; i < terrainData.alphamapLayers; i++)
                {

                    // Normalize so that sum of all texture weights = 1
                    splatWeights[i] /= z;

                    // Assign this point to the splatmap array
                    splatmapData[x, y, i] = splatWeights[i];
                }
            }
        }

        // Finally assign the new splatmap to the terrainData:
        terrainData.SetAlphamaps(0, 0, splatmapData);
    }
}
