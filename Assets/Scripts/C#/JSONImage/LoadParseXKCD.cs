using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;

public class LoadParseXKCD : JsonNetwork
{
    [SerializeField] [Tooltip("Insert the Api link in here")] private string APILink;
    [SerializeField] private RawImage myRawImage;
    [SerializeField] private TextMeshProUGUI storyTitle;

    public override void Start()
    {
        StartCoroutine(GetRequest(APILink));
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
    protected override void ParseJSON(string jsonStr)
    {
        JSONNode jsonOBJ = JSON.Parse(jsonStr);
        string titleURL = jsonOBJ["title"];
        string imgURL = jsonOBJ["img"];
        GetTitle(titleURL);
        StartCoroutine(GetImage(imgURL));
    }

    protected override void RecievedTextureHandler(Texture2D texture)
    {
    }

    /// Getting the title name
    private void GetTitle(string titleURL)
    {
         storyTitle.text = titleURL;
    }


    /// Recieve the Image.
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
                myRawImage.texture = myTexture2D;
            }

        }
    }
}
