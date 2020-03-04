using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScape : LandScape
{
    private Terrain t;

    private void Start()
    {
        t = GetComponent<Terrain>();

        if(t == null)
        {
            Debug.LogError(message: "Put the terrainScape script on a terrain");
        }
        Generate();

        Initialize();
    }
    public override void Generate()
    {

        Debug.Log("Adding height");
        t.terrainData.heightmapResolution = GameManagerRandom.instance.world.Size;
        t.terrainData.SetHeights(xBase: 0, 0, GameManagerRandom.instance.world.heights);
    }
}
