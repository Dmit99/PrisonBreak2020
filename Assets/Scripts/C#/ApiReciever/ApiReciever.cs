using UnityEngine;

public class ApiReciever : JsonNetwork
{
    [SerializeField] [Tooltip("Insert the Api link in here")] private string APILink;

    public override void Start()
    {
        StartCoroutine(GetRequest(APILink));
    }

    //IEnumerator GetRequest(string uri)
    //{
    //    using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
    //    {
    //        // Request and wait for the desired page.
    //        yield return webRequest.SendWebRequest();

    //        if (webRequest.isNetworkError)
    //        {
    //            Debug.Log(": Error: " + webRequest.error);
    //        }
    //        else
    //        {
    //            JSONNode jsonObj = JSON.Parse(webRequest.downloadHandler.text);

    //            for (int i = 0; i < jsonObj["country"].Count; i++)
    //            {
    //                Debug.Log((float)jsonObj[i]);
    //            }

    //        }
    //    }
    //}
}
