using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class JsonNetwork : MonoBehaviour
{
    public JsonNetwork() 
    {
        
    }

    public virtual void Start()
    {
        
    }

    public IEnumerator GetRequest(string uri)
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
