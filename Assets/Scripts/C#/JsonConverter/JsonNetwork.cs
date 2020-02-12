using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public abstract class JsonNetwork : MonoBehaviour
{

    protected virtual void ParseJSON(JSONNode jsonString) 
    {
        JSONNode jsonOBJ = JSON.Parse(jsonString);
    }

    /// Used for scaling! 
    protected virtual void RecievedTextureHandler(Texture2D texture) { }

    public JsonNetwork() 
    {
        
    }

    public virtual void Start()
    {
        
    }

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
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                ParseJSON(webRequest.downloadHandler.text);
            }
        }
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
                RecievedTextureHandler(myTexture2D);
            }

        }
    }
}
