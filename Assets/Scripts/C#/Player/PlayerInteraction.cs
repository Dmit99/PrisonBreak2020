using UnityEngine;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;
public class PlayerInteraction : MonoBehaviour
{
    #region variables...
    Item key;
    Item stone;

    private RaycastHit hit;
    private bool inFrontOfDoor;
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
    /// This will be used to turn on or off the player movement script. When interacting with Ui and you need the mouse you can use the ui instead of looking to somewhere else.
    private bool playercontrolled;


    [Header("UI elements")]
    [SerializeField]
    [Tooltip("Insert the information text in here.")]
    private TextMeshProUGUI information;

    [SerializeField]
    [Tooltip("Insert the Inventory in here.")]
    private GameObject uiInventory;

    [SerializeField]
    [Tooltip("Insert the content of the scrollview in here.")]
    private GameObject contentfitter;

    #endregion

    private void Start()
    {
        information.text = "";
        shootingReady = false;
        playercontrolled = true;
        uiInventory.SetActive(!playercontrolled);
    }

    void Update()
    {
        if (playercontrolled)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            this.gameObject.GetComponent<FirstPersonController>().enabled = true;
            UserInteraction();
            if (!shootingReady)
            {
                UserSelecting();
            }
            if (shootingReady)
            {
                Throwing();
            }
        }
        else if(!playercontrolled)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            this.gameObject.GetComponent<FirstPersonController>().enabled = false;
        }

        EnableInventory();
    }

    private void EnableInventory()
    {
        /// When pressing I you open up your inventory.
        if (Input.GetKeyDown(KeyCode.I))
        {
            playercontrolled = !playercontrolled;
            uiInventory.SetActive(!playercontrolled);
        }

        ///If the inventory is open you could also press escape to close it.
        if(Input.GetKeyDown(KeyCode.Escape) && !playercontrolled)
        {
            playercontrolled = !playercontrolled;
            uiInventory.SetActive(!playercontrolled);
        }
    }

    private void UserSelecting()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Inventory.instance.Selecting())
            {
                Debug.Log("There is a stone in the inventory.");
                if (Inventory.instance.Selecting())
                {
                    shootingReady = true;
                }
            }
            else 
            { 
                Debug.Log("I have nothing to throw....");
                shootingReady = false;
            }
        }
    }

    private void Throwing()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Inventory.instance.RemoveItem(item: stone);
            Instantiate(rock, fireInstance.transform.position, fireInstance.transform.rotation);
            shootingReady = false;
        }
    }

    private void UserInteraction()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            int layermask = ~LayerMask.GetMask("Player");
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask: layermask))
            {
                switch (hit.collider.name)
                {
                    /// If you hit the key.
                    case "Key":
                        var hittedObjectKey = hit.collider.gameObject.GetComponent<Key>();
                        information.text = "You have found the " + hittedObjectKey.name + " key. It wheights: " + hittedObjectKey.weight + " kilo's." + "\nYou have now " + Inventory.instance.Count() + " items in your inventory with a total wheight of: " + Inventory.instance.CheckingCurrentWheight();
                        hittedObjectKey.AddKey();
                        break;

                    /// If you hit the door.
                    case "Door":
                        int objectDoorNumber = hit.collider.gameObject.GetComponent<DoorNumber>().doorNumber;

                        if (Inventory.instance.Opens(door: objectDoorNumber))
                        {
                            if (Inventory.instance.RemoveItem(key))
                            {
                                door.GetComponent<Animator>().SetTrigger("DoorOpening");
                                information.text = ("The " + key.name + " has been used and removed from your inventory.");
                            }
                        }
                        else information.text = "The key you have /Should have isn't the right key for this door.";
                        break;

                    /// If you hit the Cofin.
                    case "Cofin1":
                        cofin1.GetComponentInChildren<Animator>().SetTrigger("Opening");
                        cofin1.GetComponent<BoxCollider>().enabled = false;
                        information.text = "You have found a pice for you boat!";
                        break;

                    /// If you hit one of the rocks.
                    case "StoneBrick":
                        var hittedObjectStone = hit.collider.gameObject.GetComponent<Rock>();
                        information.text = "You have found a " + hittedObjectStone._name + "And it weights " + hittedObjectStone.weight;
                        hittedObjectStone.AddRock();
                        break;

                    default:
                        Debug.Log(hit.collider.name);
                        break;
                }
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
