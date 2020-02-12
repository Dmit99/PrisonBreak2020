using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class ApiReciever : MonoBehaviour
{
    [SerializeField] [Tooltip("Insert the Api link in here")] private string APILink;
    /// API link: http://www.moonmoonmoonmoon.com/api/marks

    void Start()
    {
        StartCoroutine(GetRequest(APILink));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);

                JSONNode jsonObj = JSON.Parse(webRequest.downloadHandler.text);

                for (int i = 0; i < jsonObj["country"].Count; i++)
                {
                    Debug.Log((float)jsonObj[i]);
                }

            }
        }
    }
}
