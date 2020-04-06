using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryVisual : MonoBehaviour
{
    [SerializeField]
    private GameObject UIItemPrefab;

    [SerializeField]
    private GameObject content;

    public Dictionary<string, Pickup> uiItems;

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

        uiItems = new Dictionary<string, Pickup>();
    }
    #endregion

    public void RegisterPickUpItem(Pickup i)
    {
        if (!uiItems.ContainsKey(i.name))
        {
            uiItems.Add(i.name, i);
        }
    }

    public void AddUIItem(Item it)
    {
        if (!uiItems[it.name].isInInventory())
        {
            GameObject go = Instantiate(UIItemPrefab, content.transform);
            Image[] images = go.GetComponentsInChildren<Image>();

            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].name == "Image")
                {
                    images[i].sprite = uiItems[it.name].image;
                }
            }

            go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = it.name;
            uiItems[it.name].SetInventoryObj(go);
        }
    }

    public void RemoveUIItem(Item item)
    {
        if (uiItems.ContainsKey(item.name))
        {
            uiItems[item.name].Respawn();
        }
    }
}
