using UnityEngine;

public class PlayerSpawnLight : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * 100 * Time.deltaTime);
    }
}
