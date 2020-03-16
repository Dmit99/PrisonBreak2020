using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

public class BorderSeeker : MonoBehaviour
{

    [SerializeField] private GameObject player;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerBorder")
        {
            StartCoroutine(Respawning());

        }
    }

    IEnumerator Respawning()
    {
        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponent<Rigidbody>().useGravity = false;
        player.transform.localPosition = new Vector3(180, 10, 180); ///x: 180, y: 10, z: 180 is default spawn position.

        yield return new WaitForSeconds(0.5f);

        player.GetComponent<FirstPersonController>().enabled = true;
        player.GetComponent<Rigidbody>().useGravity = true;
    }
}
