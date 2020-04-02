using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Audio;
using TMPro;

public class PlayerInteraction : KeyPadScript
{
    #region variables...
    [Header("UI elements")]
    [SerializeField]
    [Tooltip("Insert the information text in here.")]
    private TextMeshProUGUI information;

    [Header("Audio")]
    [SerializeField]
    [Tooltip("Insert the audio plop sound in here.")]
    private AudioClip plopSound;
    private AudioSource thisAudioSource;

    [SerializeField]
    [Tooltip("Insert the Inventory in here.")]
    private GameObject uiInventory;

    private const float range = 5f;
    private const float maxVolume = 1f;
    private const float quarterVolume = 0.25f;
    private const float halfSecond = 0.5f;
    private const float fourAndAHalfSeconds = 4.5f;
    #endregion

    private void Awake()
    {
        thisAudioSource = gameObject.GetComponent<AudioSource>();
    }

    public override void Start()
    {
        base.Start();
        information.text = "";
        uiInventory.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetButtonDown("Action"))
        { 
            Interact();
        }

        if (Input.GetButtonDown("Inventory"))
        {
            SetInventoryVisible(!uiInventory.gameObject.activeSelf);
        }

        /// If the inventory is open you can also use the Escape button.
        if (uiInventory.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            SetInventoryVisible(!uiInventory.gameObject.activeSelf);
        }

        if (Input.GetButtonDown("ThrowRock"))
        {
            ThrowingRock();
        }
    }

    public void SetInventoryVisible(bool value)
    {
        uiInventory.SetActive(value);
        GetComponent<FirstPersonController>().enabled = !value;
        Cursor.visible = value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void ThrowingRock()
    {
        /// Removing the stone from the inventory.
        Inventory.instance.removeByName("Stone");

        /// Throwing the stone.
        
    }

    private void Interact()
    {
        Ray r = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        int ignorePlayer = ~LayerMask.GetMask("Player");

        if(Physics.Raycast(ray: r, hitInfo: out hit, maxDistance: range, layerMask: ignorePlayer))
        {
            Debug.Log(hit.collider.gameObject.name);
            switch (hit.collider.gameObject.name)
            {
                case "Key":
                    Key k = hit.collider.gameObject.GetComponent<Key>();
                    if(k != null)
                    {
                        k.Action();
                        StartCoroutine(DisplayText(pickedup: 2));
                        Inventory.instance.printToConsole();
                    }
                    break;

                case "Stone":
                    Rock ro = hit.collider.gameObject.GetComponent<Rock>();
                    if(ro != null)
                    { 
                        ro.Action();
                        StartCoroutine(DisplayText(pickedup: 3));
                        Inventory.instance.printToConsole();
                    }
                    break;

                case "Door":
                    Door d = hit.collider.gameObject.GetComponent<Door>();
                    if(d != null)
                    {
                        d.Open();
                        Inventory.instance.printToConsole();
                    }
                    break;

                case "Cigarette":
                    Bonus b = hit.collider.gameObject.GetComponent<Bonus>();
                    if(b != null)
                    {
                        b.Action();
                        StartCoroutine(DisplayText(pickedup: 1));
                        Inventory.instance.printToConsole();
                    }
                    break;

                #region KeyPadNumbers
                case "1":
                    InsertNumber(hit.collider.gameObject.name);
                    ChangeNumberLayout();
                    break;

                case "2":
                    InsertNumber(hit.collider.gameObject.name);
                    ChangeNumberLayout();
                    break;

                case "3":
                    InsertNumber(hit.collider.gameObject.name);
                    ChangeNumberLayout();
                    break;

                case "4":
                    InsertNumber(hit.collider.gameObject.name);
                    ChangeNumberLayout();
                    break;

                case "5":
                    InsertNumber(hit.collider.gameObject.name);
                    ChangeNumberLayout();
                    break;

                case "6":
                    InsertNumber(hit.collider.gameObject.name);
                    ChangeNumberLayout();
                    break;

                case "7":
                    InsertNumber(hit.collider.gameObject.name);
                    ChangeNumberLayout();
                    break;

                case "8":
                    InsertNumber(hit.collider.gameObject.name);
                    ChangeNumberLayout();
                    break;

                case "9":
                    InsertNumber(hit.collider.gameObject.name);
                    ChangeNumberLayout();
                    break;

                case "0":
                    InsertNumber(hit.collider.gameObject.name);
                    ChangeNumberLayout();
                    break;

                case "Enter":
                    NumberCorrectChecker();
                    break;
                #endregion

                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Display the text on the screen for player notifying.
    /// </summary>
    /// <param name="pickedup">If picked up is: 1, picked up is a bonus item. If picked up is 2, its a key!</param>
    /// <returns></returns>
    private IEnumerator DisplayText(int pickedup)
    {
        switch (pickedup)
        {
            case 1:
                thisAudioSource.volume = quarterVolume;
                thisAudioSource.PlayOneShot(plopSound);
                information.text = "You have found a cigarette. \nIt weights 0.2 kg.";
                yield return new WaitForSeconds(halfSecond);
                thisAudioSource.volume = maxVolume;
                yield return new WaitForSeconds(fourAndAHalfSeconds);
                information.text = "";
                break;

            case 2:
                thisAudioSource.volume = quarterVolume;
                thisAudioSource.PlayOneShot(plopSound);
                information.text = "You have found a key! \nYou could try to unlock a door with this one.";
                yield return new WaitForSeconds(halfSecond);
                thisAudioSource.volume = maxVolume;
                yield return new WaitForSeconds(fourAndAHalfSeconds);
                information.text = "";
                break;

            case 3:
                thisAudioSource.volume = quarterVolume;
                thisAudioSource.PlayOneShot(plopSound);
                information.text = "You picked up a rock. \nPress G to throw it!\nThrow it wisely!";
                yield return new WaitForSeconds(halfSecond);
                thisAudioSource.volume = maxVolume;
                yield return new WaitForSeconds(fourAndAHalfSeconds);
                information.text = "";
                break;

            default:
                break;
        }
    }
    
}
