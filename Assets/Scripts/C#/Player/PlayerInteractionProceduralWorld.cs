using UnityEngine;
using System;
using System.Collections;


public class PlayerInteractionProceduralWorld : MonoBehaviour
{

    [SerializeField] private AudioClip plopSound;

    private AudioSource thisAudioSource;
    private string gameInformation;

    private const float maxRange = 10;
    private const int ADDEDPART = 1;
    private const float maxVolume = 1f;
    private const float quarterVolume = 0.25f;
    private const float halfSecond = 0.5f;
    private const float fourAndAHalfSeconds = 4.5f;

    private void Awake()
    {
        thisAudioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
           StartCoroutine(Raycasting());
        }
    }

    private IEnumerator Raycasting()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        int ignorePlayer = ~LayerMask.GetMask("Player");

        if (Physics.Raycast(ray, hitInfo: out hit, maxDistance: maxRange, layerMask: ignorePlayer))
        {
            switch (hit.collider.gameObject.name)
            {
                case "OldWoodenRowboat(Clone)":

                    thisAudioSource.volume = quarterVolume;
                    thisAudioSource.PlayOneShot(plopSound);

                    UIManager.instance.UpdateUITotalPartsInformation(ADDEDPART);
                    gameInformation = "You have found a wooden boat!";

                    UIManager.instance.UpdateUIGameInformation(gameInformation);
                    Destroy(hit.collider.gameObject);

                    yield return new WaitForSeconds(halfSecond);
                    thisAudioSource.volume = maxVolume;

                    yield return new WaitForSeconds(fourAndAHalfSeconds);
                    gameInformation = "";
                    break;

                case "Boat_Covered(Clone)":

                    thisAudioSource.volume = quarterVolume;
                    thisAudioSource.PlayOneShot(plopSound);

                    UIManager.instance.UpdateUITotalPartsInformation(ADDEDPART);
                    gameInformation = "You have found a boat cover!";

                    UIManager.instance.UpdateUIGameInformation(gameInformation);
                    Destroy(hit.collider.gameObject);

                    yield return new WaitForSeconds(halfSecond);
                    thisAudioSource.volume = maxVolume;

                    yield return new WaitForSeconds(fourAndAHalfSeconds);
                    gameInformation = "";
                    break;

                case "Paddle(Clone)":

                    thisAudioSource.volume = quarterVolume;
                    thisAudioSource.PlayOneShot(plopSound);

                    UIManager.instance.UpdateUITotalPartsInformation(ADDEDPART);
                    gameInformation = "You have found a paddle!";

                    UIManager.instance.UpdateUIGameInformation(gameInformation);
                    Destroy(hit.collider.gameObject);

                    yield return new WaitForSeconds(halfSecond);
                    thisAudioSource.volume = maxVolume;
                    yield return new WaitForSeconds(fourAndAHalfSeconds);
                    gameInformation = "";
                    break;

                default:
                    break;
            }
        }
    }
}
