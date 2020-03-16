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

    public override void Clean()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Rocks");
        for (int i = 0; i < gos.Length; i++)
        {
            Destroy(gos[i]);
        }
    }

    public override void Generate()
    {
        Clean();
        t.terrainData.heightmapResolution = GameManagerRandom.instance.world.Size;
        t.terrainData.SetHeights(xBase: 0, 0, GameManagerRandom.instance.world.heights);
        /// Loading textures
        
        /// Instantiating Assets on the terrain.
        for(int r= 0; r < GameManagerRandom.instance.world.assets.Count; r++)
        {
            Vector3Int asset = GameManagerRandom.instance.world.assets[r];

            Vector3 worldPosition = new Vector3(
               x: MathUtils.Map(
                    asset.x,
                    0,
                    GameManagerRandom.instance.world.Size,
                    t.GetPosition().x,
                    t.GetPosition().x + t.terrainData.size.x),

                y: 0.0f,

                z: MathUtils.Map(
                    asset.y,
                    0,
                    GameManagerRandom.instance.world.Size,
                    t.GetPosition().z,
                    t.GetPosition().z + t.terrainData.size.z)

                );

            worldPosition.y = t.SampleHeight(worldPosition);

            if (worldPosition.y > 0.005f)
            {
                Instantiate(original: GameManagerRandom.instance.world.assetsPfb[asset.z], position: worldPosition, rotation: Quaternion.identity);
            }
        }

    }
}
