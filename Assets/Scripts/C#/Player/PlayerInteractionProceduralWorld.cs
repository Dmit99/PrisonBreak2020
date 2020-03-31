using UnityEngine;

public class PlayerInteractionProceduralWorld : MonoBehaviour
{
    private const int ADDEDPART = 1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Raycasting();
        }
    }

    private void Raycasting()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        float maxRange = 10;
        int ignorePlayer = ~LayerMask.GetMask("Player");
        string gameInformation;


        if (Physics.Raycast(ray, hitInfo: out hit, maxDistance: maxRange, layerMask: ignorePlayer))
        {
            switch (hit.collider.gameObject.name)
            {
                case "OldWoodenRowboat(Clone)":
                    UIManager.instance.UpdateUITotalPartsInformation(ADDEDPART);
                    gameInformation = "You have found a wooden boat!";
                    UIManager.instance.UpdateUIGameInformation(gameInformation);

                    Destroy(hit.collider.gameObject);
                    break;

                case "Boat_Covered(Clone)":
                    UIManager.instance.UpdateUITotalPartsInformation(ADDEDPART);
                    gameInformation = "You have found a boat cover!";
                    UIManager.instance.UpdateUIGameInformation(gameInformation);

                    Destroy(hit.collider.gameObject);
                    break;

                case "Paddle(Clone)":
                    UIManager.instance.UpdateUITotalPartsInformation(ADDEDPART);
                    gameInformation = "You have found a paddle!";
                    UIManager.instance.UpdateUIGameInformation(gameInformation);

                    Destroy(hit.collider.gameObject);
                    break;

                default:
                    break;
            }
        }
    }
}
