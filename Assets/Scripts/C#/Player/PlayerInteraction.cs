using UnityEngine;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerInteraction : KeyPadScript
{
    #region variables...
    private float range = 5f;

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
        Starting();

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

                case "Door":
                    Door d = hit.collider.gameObject.GetComponent<Door>();
                    if(d != null)
                    {
                        d.Open();
                        Inventory.instance.printToConsole();
                    }
                    break;

                case "BonusItem":
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
}
