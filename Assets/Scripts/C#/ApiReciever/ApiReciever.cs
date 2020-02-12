using UnityEngine;
using SimpleJSON;


public class ApiReciever : JsonNetwork
{
    [SerializeField] [Tooltip("Insert the Api link in here")] private string APILink;

    public override void Start()
    {
        StartCoroutine(base.GetRequest(APILink));
    }

    protected override void ParseJSON(JSONNode jsonString)
    {
        for (int i = 0; i < jsonString["country"].Count; i++)
        {
            Debug.Log((float)jsonString[i]);
        }
    }
}
