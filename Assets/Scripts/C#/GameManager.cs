using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton...
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != null)
        {
            Destroy(this);
        }
    }
    #endregion

    [Tooltip("Insert the gate of the jail here.")]
    [SerializeField] private GameObject gate;

    public bool OpenGate;

    void Update()
    {
        /// If the stone hitted the bell.
        if (OpenGate)
        {
            gate.SetActive(false);
        }
    }
}
