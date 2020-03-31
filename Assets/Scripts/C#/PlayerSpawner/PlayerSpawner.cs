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

    private void Start()
    {
        StartCoroutine(PlayerToStartPosition());
    }

    private IEnumerator PlayerToStartPosition()
    {
        CenterCalculation();
        yield return new WaitForSeconds(0.25f);
        fps.enabled = true;
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

    private void OnTriggerEnter(Collider other)
    {
        /// If player hits the border of the map he will be teleported back to the spawn position.
        if (other.gameObject.tag == "Border")
        {
            fps.enabled = false;
            StartCoroutine(PlayerToStartPosition());
        }
    }
}
