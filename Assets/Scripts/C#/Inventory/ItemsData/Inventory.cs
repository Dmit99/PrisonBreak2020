using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null)
        {
            Destroy(this);
        }

        items = new List<Item>();
    }
    #endregion

    public List<Item> items;
    [SerializeField] private float maxWeight = 10f;
    [SerializeField] private float currentWeight;

    private void Start()
    {
        currentWeight = 0f;
    }

    public bool AddItem(Item item)
    {
        if (currentWeight + item.weight <= maxWeight)
        {
            items.Add(item);
            InventoryVisual.instance.AddUIItem(item);
            currentWeight += item.weight;
            return true;
        }
        else return false;
    }

    public bool RemoveItem(Item item)
    {
        if (items.Remove(item))
        {
            InventoryVisual.instance.RemoveUIItem(item);
            currentWeight -= item.weight;
            return true;
        }
        else
        {
            Debug.Log("You can't remove this item. Your current weight is: " + currentWeight);
            return false;
        }
    }

    public int Count()
    {
        return items.Count;
    }

    public void removeByName(string name)
    {
        foreach (Item i in items)
        {
            if(i.name == name)
            {
                RemoveItem(i);
                break;
            }
        }
    }

    public bool HasKey(int id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i] is AccesItem)
            {
                AccesItem it = (AccesItem)items[i];
                if(it.doorId == id)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public float CheckingCurrentWheight()
    {
        return currentWeight;
    }

    /// Selecting the stone.
    public bool Selecting()
    {
        foreach (Item i in items)
        {
            if (i is ThrowableItem)
            {
                ThrowableItem tI = (ThrowableItem)i;
                return true;
            }
        }
        return false;
    }

    public void printToConsole()
    {
        foreach (Item i in items)
        {
            Debug.Log(i.name + "--" + i.weight);
        }

        Debug.Log("Current Weight: " + currentWeight);
    }
}
