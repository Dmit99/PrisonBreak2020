using UnityEngine;

public interface IInteractable
{ 
    void AddToInventory(Item i);

    void Delete(GameObject go);
}
