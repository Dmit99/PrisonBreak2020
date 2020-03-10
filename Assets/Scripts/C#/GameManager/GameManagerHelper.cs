using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region no default name spaces
using UnityEngine.Networking;
using UnityEngine.UI;
#endregion

namespace gamehelper
{
    public class GameManagerHelper : MonoBehaviour
    {
        #region audio
        [Header("Audio")]
        private AudioSource bellAudio;
        public AudioClip alarm;
        public AudioClip bell;
        #endregion

        #region prison lights
        [Header("Prison lights")]
        public bool lightsOn;
        public GameObject[] AlarmLight = new GameObject[17];
        #endregion

        #region keySetup
        [SerializeField] private GameObject keyObj;
        [SerializeField] private List<Transform> keySpawnLocations;
        #endregion

        #region gates and doors.
        [Header("gates and doors")]
        [Tooltip("Insert the gate of the jail here.")]
        [SerializeField] private GameObject gate;
        public bool OpenGate;
        #endregion

        protected virtual void Start()
        {
            bellAudio = GameObject.Find("Bell").GetComponentInChildren<AudioSource>();
        }

        protected virtual void Update()
        {
            /// If the stone hitted the bell.
            if (OpenGate)
            {
                StartCoroutine(PunchAlarm());
                gate.SetActive(false);
                OpenGate = false;
            }

            /// When the lights of the jail are on.
            if (lightsOn)
            {
                for (int i = 0; i < 9; i++)
                {
                    AlarmLight[i].transform.Rotate(0, 0, 60 * Time.deltaTime * 2);
                }

                for (int i = 9; i < 17; i++)
                {
                    AlarmLight[i].transform.Rotate(0, 0, -60 * Time.deltaTime * 2);
                }
            }
        }

        protected IEnumerator GetPNG(string pngUrl, RawImage qrCodeImage)
        {
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(pngUrl))
            {
                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    Debug.Log(uwr.error);
                }
                else
                {
                    Texture2D myTexture2D = DownloadHandlerTexture.GetContent(uwr);
                    qrCodeImage.texture = myTexture2D;
                }
            }
        }

        protected void SetupAlarmLights()
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

        protected void SetupKeyLocation()
        {
            int randomSpawnLocation = Random.Range(0, keySpawnLocations.Count);
            keyObj.transform.position = keySpawnLocations[randomSpawnLocation].transform.position;
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
    }
}
