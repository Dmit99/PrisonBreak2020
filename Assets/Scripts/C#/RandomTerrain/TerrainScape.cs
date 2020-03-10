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

        for(int r= 0; r < GameManagerRandom.instance.world.rocks.Count; r++)
        {
            Vector3Int rock = GameManagerRandom.instance.world.rocks[r];

            Vector3 worldPosition = new Vector3(
               x: MathUtils.Map(
                    rock.x,
                    0,
                    GameManagerRandom.instance.world.Size,
                    t.GetPosition().x,
                    t.GetPosition().x + t.terrainData.size.x),

                y: 0.0f,

                z: MathUtils.Map(
                    rock.y,
                    0,
                    GameManagerRandom.instance.world.Size,
                    t.GetPosition().z,
                    t.GetPosition().z + t.terrainData.size.z)

                );

            worldPosition.y = t.SampleHeight(worldPosition);

            Instantiate(original: GameManagerRandom.instance.world.rockPrefab[rock.z], position: worldPosition, rotation: Quaternion.identity);
        }

    }
}
