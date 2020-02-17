using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    private RawImage qRcodeCanvas;
    public AudioClip alarm;
    public AudioClip bell;
    private AudioSource bellAudio;
    public GameObject[] AlarmLight = new GameObject[12];
    public bool lightsOn;

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
        /// Getting QR code image.
        StartCoroutine(GetImage("https://chart.googleapis.com/chart?cht=qr&chs=300x300&chl=otpauth%3A%2F%2Ftotp%2FDmitri_The_Prisoner%3Fsecret%3DGM2DENRSG5BVSS2B%26issuer%3DPrisonBreak2020"));
        bellAudio = GameObject.Find("Bell").GetComponentInChildren<AudioSource>();
        SetupAlarmLights();
    }

    void Update()
    {
        /// If the stone hitted the bell.
        if (OpenGate)
        {
            StartCoroutine(PunchAlarm());
            gate.SetActive(false);
            OpenGate = false;
        }

        if (lightsOn)
        {
            AlarmLight[0].transform.Rotate(0, 0, 60 * Time.deltaTime * 2);
            AlarmLight[1].transform.Rotate(0, 0, 60 * Time.deltaTime * 2);
            AlarmLight[2].transform.Rotate(0, 0, 60 * Time.deltaTime * 2);
            AlarmLight[3].transform.Rotate(0, 0, 60 * Time.deltaTime * 2);
            AlarmLight[4].transform.Rotate(0, 0, 60 * Time.deltaTime * 2);
            AlarmLight[5].transform.Rotate(0, 0, 60 * Time.deltaTime * 2);

            AlarmLight[6].transform.Rotate(0, 0, -60 * Time.deltaTime * 2);
            AlarmLight[7].transform.Rotate(0, 0, -60 * Time.deltaTime * 2);
            AlarmLight[8].transform.Rotate(0, 0, -60 * Time.deltaTime * 2);
            AlarmLight[9].transform.Rotate(0, 0, -60 * Time.deltaTime * 2);
            AlarmLight[10].transform.Rotate(0, 0, -60 * Time.deltaTime * 2);
            AlarmLight[11].transform.Rotate(0, 0, -60 * Time.deltaTime * 2);
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

    IEnumerator PunchAlarm()
    {
        bellAudio.volume = 1;
        bellAudio.PlayOneShot(bell);
        yield return new WaitForSeconds(5);
        bellAudio.volume = 0.5f;
        bellAudio.loop = true;
        bellAudio.clip = alarm;
        bellAudio.Play();
        foreach (GameObject light in AlarmLight)
        {
            /// Changing the material of the object to red.
            light.GetComponent<MeshRenderer>().material.color = Color.red;
            light.GetComponent<MeshRenderer>().material.SetColor("_EMISSION", Color.red);
            light.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            Light[] spotlight;
            spotlight = light.GetComponentsInChildren<Light>();
            foreach (Light spotLight in spotlight)
            {
                spotLight.GetComponentInChildren<Light>().enabled = true;
                spotLight.GetComponentInChildren<Light>().color = Color.red;
            }
        }
        lightsOn = true;
    }

    private void SetupAlarmLights()
    {
        for (int i = 0; i < AlarmLight.Length; i++)
        {
            AlarmLight[i] = GameObject.Find("Alarm" + i);
            AlarmLight[i].GetComponentInChildren<Light>().enabled = false;

            /// Turning off every light inside the lights
            Light[] spotlight;
            spotlight = AlarmLight[i].GetComponentsInChildren<Light>();
            foreach (Light spotLight in spotlight)
            {
                spotLight.GetComponentInChildren<Light>().enabled = false;
            }
        }
    }
}
