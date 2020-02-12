using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRock : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        this.gameObject.name = "StoneBrick";

        /// Searching the rigidbody component. 
        rb = this.gameObject.GetComponent<Rigidbody>();

        /// Adding Rigidbody force.
        Vector3 push = (Camera.main.transform.forward * 600 + Vector3.up * 100) * Time.deltaTime;
        rb.AddForce(push * 60);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "church_bell")
        {
            GameManager.instance.OpenGate = true;
        }
    }
}
