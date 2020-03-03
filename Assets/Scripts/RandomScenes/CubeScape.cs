using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScape : MonoBehaviour
{
    [SerializeField] private GameObject pfb;
    [SerializeField] int seedNumber;

    public float maxHeight = 5f;
    public float minHeight = 0f;
    public float gridSize = 50f;
    public float detailed = 10.0f;

    private void Start()
    {
        GameManagerRandom.instance.SetSeed(seedNumber);
        InstanciteCube();
    }

    public void InstanciteCube()
    {

        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                Random.InitState(seed: seedNumber); //Random generation
                float perlinX = (x / detailed) + GameManagerRandom.instance.GetPerlinSeed();
                float perlinZ = (z / detailed) + + GameManagerRandom.instance.GetPerlinSeed();
                float r = (Mathf.PerlinNoise(perlinX, perlinZ)-minHeight)*maxHeight;
                Vector3 pos = new Vector3(x: x, y: r, z: z);
                Instantiate(pfb, transform.position + pos, Quaternion.identity, transform);
            }
        }
    }
}
