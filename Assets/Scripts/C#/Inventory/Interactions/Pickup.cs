using UnityEngine;

public abstract class Pickup : MonoBehaviour, IInteractable
{
    public string _name;
    public float weight;
    public Sprite image;
    private GameObject inventoryObj;
    protected abstract Item CreateItem();
    private Vector3 force;

    private void Start()
    {
        InventoryVisual.instance.RegisterPickUpItem(this);
    }

    public void Action()
    {
        if (Inventory.instance.AddItem(CreateItem()))
        {
            gameObject.SetActive(false);
        }
    }

    public bool isInInventory()
    {
        return inventoryObj != null;
    }

    public void SetInventoryObj(GameObject go)
    {
        inventoryObj = go;
    }

    public void Delete(GameObject go)
    {
        go.gameObject.SetActive(false);
    }

    public void RemoveInventoryObj()
    {
        Destroy(inventoryObj);
        inventoryObj = null;
    }

    public void Respawn()
    {
        /// Checking if the object is a stone.
        if (gameObject.name == "Stone")
        {
            RemoveInventoryObj();
            transform.position = Camera.main.transform.position + Camera.main.transform.forward;
            gameObject.SetActive(true);
            force = (Camera.main.transform.forward * 600 + Vector3.up * 100);
            gameObject.GetComponent<Rigidbody>().AddForce(force);
            Debug.Log("Done!");
        }
        else
        {
            /// Put this gameobject back in the world
            /// put it here the player is right now.
            RemoveInventoryObj();
            transform.position = Camera.main.transform.position + Camera.main.transform.forward;
            gameObject.SetActive(true);
        }
    }
}
