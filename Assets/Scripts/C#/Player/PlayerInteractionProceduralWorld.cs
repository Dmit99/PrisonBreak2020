using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionProceduralWorld : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Raycasting()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        float maxRange = 10;
        int ignorePlayer = ~LayerMask.GetMask("Player");

        if (Physics.Raycast(ray, hitInfo: out hit, maxDistance: maxRange, layerMask: ignorePlayer))
        {
            switch (hit.collider.gameObject.name)
            {
                case "OldWoodenRowboat(Clone)":

                    break;

                case "Boat_Covered(Clone)":

                    break;

                case "Paddle(Clone)":

                    break;

                default:
                    break;
            }
        }
    }
}
