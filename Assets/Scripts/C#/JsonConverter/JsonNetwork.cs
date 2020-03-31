using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public abstract class JsonNetwork : MonoBehaviour
{

    protected virtual void ParseJSON(string jsonString) { }

    /// Used for scaling! 
    protected virtual void RecievedTextureHandler(Texture2D texture) { }

    public JsonNetwork() 
    {
        
    }
    public virtual void Start() { }

    protected virtual IEnumerator GetRequest(string uri)
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
                ParseJSON(webRequest.downloadHandler.text);
            }
        }
    }
}
