using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerRandom : MonoBehaviour
{
    #region singleton
    public static GameManagerRandom instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != null)
        {
            Destroy(obj: this);
        }
    }
    #endregion

    private int seed;
    private float perlinSeed;

    void Start()
    {

    }

    public void SetSeed(int seed)
    {
        this.seed = seed;
        Random.InitState(seed);
        perlinSeed = Random.Range(-100000, 100000);
    }

    public float GetPerlinSeed()
    {
        return perlinSeed;
    }
}
