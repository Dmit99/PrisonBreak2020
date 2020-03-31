using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
        uiManager.StartUISetup();
    }
    #endregion

    private int m_partsNumber = 0;
    private int seed;
    private float perlinSeed;
    public UnityEvent regenerate;
    public ProceduralWorld world;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cutSceneCamera;
    [SerializeField] private GameObject boat;

    private void Start()
    {
        boat.SetActive(false);
    }

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

    public int GetPartsNumber()
    {
        return m_partsNumber;
    }

    public int AddPartsNumber(int addedValue)
    {
        m_partsNumber += addedValue;
        return m_partsNumber;
    }

    public IEnumerator StartEndScene()
    {
        player.SetActive(false);
        boat.SetActive(true);
        boat.GetComponent<Animator>().SetBool(name: "CutSceneStart", value: true);
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
