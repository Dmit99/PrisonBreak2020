using UnityEngine;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;
public class PlayerInteraction : MonoBehaviour
{
    #region variables...
    private bool inFrontOfDoor;
    private float range = 5f;

    [Header("GameObjects")]
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private GameObject cofin1;
    [SerializeField]
    private GameObject rock;
    [SerializeField]
    private GameObject fireInstance;

    /// This will be true once the player has a throwable object in he's inventory.
    private bool shootingReady;


    [Header("UI elements")]
    [SerializeField]
    [Tooltip("Insert the information text in here.")]
    private TextMeshProUGUI information;

    [SerializeField]
    [Tooltip("Insert the Inventory in here.")]
    private GameObject uiInventory;

    #endregion

    private void Start()
    {
        information.text = "";
        shootingReady = false;
        uiInventory.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Action"))
        { 
            Interact();
            Debug.Log("Action is active!");
        }

        if (Input.GetButtonDown("Inventory"))
        {
            SetInventoryVisible(!InventoryVisual.instance.gameObject.activeSelf);
        }
    }

    public void SetInventoryVisible(bool value)
    {
        uiInventory.SetActive(value);
        GetComponent<FirstPersonController>().enabled = !value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
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
                        Inventory.instance.printToConsole();
                    }
                    break;

                case "Stone":
                    Rock ro = hit.collider.gameObject.GetComponent<Rock>();
                    if(ro != null)
                    { 
                        ro.Action();
                        Inventory.instance.printToConsole();
                    }
                    break;

                case "BonusItem":
                    break;

                default:
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DoorTrigger")
        {
            inFrontOfDoor = true;
            Debug.Log(inFrontOfDoor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "DoorTrigger")
        {
            inFrontOfDoor = false;
            Debug.Log(inFrontOfDoor);
        }
    }
}
