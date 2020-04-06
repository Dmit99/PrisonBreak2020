using UnityEngine;

public class FlyingRock : MonoBehaviour
{
    [Header("Audio clips")]
    [SerializeField] private AudioClip plastichit;
    [SerializeField] private AudioClip groundhit;
    [SerializeField] private AudioClip wallhit;
    [SerializeField] private AudioSource audios;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Bell")
        {
            GameManager.instance.OpenGate = true;
        }

        if(collision.gameObject.name == "Lamp" && collision.relativeVelocity.magnitude > 0.1f)
        {
            audios.PlayOneShot(plastichit);
        }

        if (collision.gameObject.tag == "Floor" && collision.relativeVelocity.magnitude > 0.1f)
        {
            audios.PlayOneShot(groundhit);
        }

        if (collision.gameObject.name == "Pole" || collision.gameObject.name == "Wall")
        {
            if (collision.relativeVelocity.magnitude > 0.1f)
            {
                audios.PlayOneShot(wallhit);
            }
        }
    }
}
