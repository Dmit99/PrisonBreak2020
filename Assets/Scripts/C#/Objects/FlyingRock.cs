using UnityEngine;

public class FlyingRock : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Bell")
        {
            GameManager.instance.OpenGate = true;
        }
    }
}
