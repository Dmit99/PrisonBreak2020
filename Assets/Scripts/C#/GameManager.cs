using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    private RawImage qRcodeCanvas;

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

        qRcodeCanvas = GameObject.Find("WallWithQRCode").GetComponentInChildren<Canvas>().GetComponentInChildren<RawImage>();
    }
    #endregion

    [Tooltip("Insert the gate of the jail here.")]
    [SerializeField] private GameObject gate;

    public bool OpenGate;

    private void Start()
    {
        StartCoroutine(GetImage("https://chart.googleapis.com/chart?cht=qr&chs=300x300&chl=otpauth%3A%2F%2Ftotp%2FDmitri_The_Prisoner%3Fsecret%3DGM2DENRSG5BVSS2B%26issuer%3DPrisonBreak2020"));
    }
    void Update()
    {
        /// If the stone hitted the bell.
        if (OpenGate)
        {
            gate.SetActive(false);
        }
    }

    private IEnumerator GetImage(string uri)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(uri))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                Texture2D myTexture2D = DownloadHandlerTexture.GetContent(uwr);
                qRcodeCanvas.texture = myTexture2D;
            }
        }
    }
}
