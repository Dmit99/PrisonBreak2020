using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUiItem : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI itemName;

    public void RegisterPickup(string name, Sprite icon)
    {
        itemImage.sprite = icon;
        itemName.text = name;
        this.name = name;
    }
}
