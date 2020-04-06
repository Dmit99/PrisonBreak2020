using UnityEngine;
using SimpleJSON;

public class ApiReciever : JsonNetwork
{
    [SerializeField] [Tooltip("Insert the Api link in here")] private string APILink;
    [SerializeField] [Tooltip("Insert the steam api link in here")] private string SteamApiLink;

    public override void Start()
    {
        StartCoroutine(base.GetRequest(APILink));
        StartCoroutine(base.GetRequest(SteamApiLink));
    }

    protected override void ParseJSON(string jsonString)
    {
        JSONNode jsonOBJ = JSON.Parse(jsonString);
        for (int i = 0; i < jsonOBJ["Count"].Count; i++)
        {
            Debug.Log(jsonOBJ[i]);
        }
    }
}
