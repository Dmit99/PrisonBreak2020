using UnityEngine;
using System.Collections;

public class DoorAPI : MonoBehaviour
{
    private float initialRotation;
    public string openDoor;

    public void Start()
    {
        initialRotation = transform.rotation.eulerAngles.y;
    }
    private void Update()
    {
        if (openDoor == "True" && transform.rotation.eulerAngles.y < initialRotation + 80)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, initialRotation + 80, 0), 5);
            GameManager.instance.ChangeScene(2);
        }
        else if (openDoor == "False" && transform.rotation.eulerAngles.y > initialRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, initialRotation, 0), 5);
        }
    }
}
