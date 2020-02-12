using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryButtonHandler : MonoBehaviour
{
    private PlayerInteraction player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
    }

    public void HandleClick()
    {
        //Debug.Log("Removing " + transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
        Inventory.instance.removeByName(transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
        player.SetInventoryVisible(false);
    }
}
