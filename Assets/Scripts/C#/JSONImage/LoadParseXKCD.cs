using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;

public class LoadParseXKCD : MonoBehaviour
{
    [SerializeField] [Tooltip("Insert the Api link in here")] private string APILink;
    [SerializeField] private RawImage myRawImage;
    [SerializeField] private TextMeshProUGUI storyTitle;

    void Start()
    {
        StartCoroutine(GetRequest(APILink));
    }

    IEnumerator GetRequest(string apiURL)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiURL))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            { 
                /// Recieved webrequest and parsing it.
                parseJSON(webRequest.downloadHandler.text);
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeComic();
        }

    }

    private void ChangeComic()
    {
        int number = Random.Range(0, 1000);
        APILink = "https://xkcd.com/" + number.ToString() + "/info.0.json";
        StartCoroutine(GetRequest(APILink));
    }


    /// Recieving the json script volume.
    private void parseJSON(string jsonStr)
    {
        JSONNode jsonOBJ = JSON.Parse(jsonStr);
        string imageURL = jsonOBJ["img"];
        string titleURL = jsonOBJ["title"];
        StartCoroutine(GetTexture(imageURL));
        GetTitle(titleURL);
    }

    private IEnumerator GetTexture(string imgUrl)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(imgUrl))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                // Get downloaded asset bundle
                Texture2D myTexture2D = DownloadHandlerTexture.GetContent(uwr);
                myRawImage.texture = myTexture2D;
            }
        }
    }

    /// Getting the title name
    private void GetTitle(string titleURL)
    {
         storyTitle.text = titleURL;
    }
}
