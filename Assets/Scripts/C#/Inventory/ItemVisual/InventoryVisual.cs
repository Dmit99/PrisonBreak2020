using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryVisual : MonoBehaviour
{
    [SerializeField]
    private Transform UIItemPrefab;

    [SerializeField]
    private GameObject content;

    public List<Pickup> uiItems;

    #region Singleton...
    public static InventoryVisual instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != null)
        {
            Destroy(this);
        }

        uiItems = new List<Pickup>();
    }
    #endregion

    public void AddUIItem(Pickup p)
    {
        if (!uiItems.Contains(p))
        {
            Transform t = Instantiate(UIItemPrefab, transform);
            InventoryUiItem i = t.GetComponent<InventoryUiItem>();
            if(i != null)
            {
                i.RegisterPickup(p.item.name, p.inventoryIcon);
                uiItems.Add(p);
                t.parent = content.transform;
            }
        }
    }

    public void RemoveUIItem(Item item)
    {
        for (int i = 0; i < uiItems.Count; i++)
        {
            Pickup p = uiItems[i];
            if(p.item.name == item.name)
            {
                /// Deze moeten we verwijderen
                uiItems.Remove(p);
                break;
            }
        }
    }
}
