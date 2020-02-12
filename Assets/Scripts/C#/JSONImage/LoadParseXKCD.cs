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
    protected override void ParseJSON(JSONNode jsonStr)
    {
        base.ParseJSON(jsonStr);
        string titleURL = jsonStr["title"];
        GetTitle(titleURL);
    }

    protected override void RecievedTextureHandler(Texture2D texture)
    {
        myRawImage.texture = texture;
    }

    /// Getting the title name
    private void GetTitle(string titleURL)
    {
         storyTitle.text = titleURL;
    }
}
