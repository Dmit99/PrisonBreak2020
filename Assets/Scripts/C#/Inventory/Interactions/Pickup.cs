using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable
{
    protected Item key;
    protected Item rock;
    protected Item gem;
    public GameObject item;

    public string _name;
    public float weight;
    public Sprite inventoryIcon;


    public virtual void Start()
    {
        
    }

    public void AddToInventory(Item i)
    {
        Inventory.instance.AddItem(i);
        InventoryVisual.instance.AddUIItem(this);
        gameObject.SetActive(false);
    }

    public void Delete(GameObject go)
    {
        go.gameObject.SetActive(false);
    }

    public void Respawn()
    {
        /// Put this gameobject back in the world
        /// put it here the player is right now.

        gameObject.SetActive(true);
        transform.position = Camera.main.transform.position + Camera.main.transform.forward; 
    }
}
