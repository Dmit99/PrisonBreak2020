using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerSpawner : MonoBehaviour
{
    FirstPersonController fps;

    private void Awake()
    {
        fps = this.gameObject.GetComponent<FirstPersonController>();
        fps.enabled = false;
    }

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        CenterCalculation();
        yield return new WaitForEndOfFrame();
        fps.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CenterCalculation() 
    {     
        Terrain t = Terrain.activeTerrain;
        Vector3 center = t.GetPosition() + (t.terrainData.size / 2f);
        float height = t.SampleHeight(center);
        Vector3 pos = new Vector3(center.x, height + 2f, center.z);
        this.gameObject.transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
        Debug.Log(this.gameObject.transform.position);
        fps.transform.position = pos;
    }
}
