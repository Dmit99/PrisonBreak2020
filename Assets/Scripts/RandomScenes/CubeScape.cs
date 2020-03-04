using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScape : MonoBehaviour
{
    [SerializeField] private GameObject pfb;

    private void Start()
    {
        GameManagerRandom.instance.regenerate.AddListener(Generate);
        Generate();
    }

    public void Clean()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void Generate()
    {
        Clean(); /// Cleans up first then generate the new ones.

        for (int x = 0; x < GameManagerRandom.instance.world.heights.GetLength(dimension: 0); x++)
        {
            for (int z = 0; z < GameManagerRandom.instance.world.heights.GetLength(dimension: 1); z++)
            {
                float height = GameManagerRandom.instance.world.heights[x, z];
                Vector3 pos = new Vector3(x: x, y: height, z: z);
                Instantiate(pfb, transform.position + pos, Quaternion.identity, transform);
            }
        }
    }
}
