using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

        regenerate = new UnityEvent();
        world.Initialize();
    }
    #endregion

    private int seed;
    private float perlinSeed;
    public UnityEvent regenerate;
    public ProceduralWorld world;

    public void SetSeed(int seed)
    {
        this.seed = seed;
        Random.InitState(seed);
        perlinSeed = Random.Range(-100000, 100000);
    }

    private void OnValidate()
    {
        regenerate.Invoke();
    }

    public float GetPerlinSeed()
    {
        return perlinSeed;
    }
}
