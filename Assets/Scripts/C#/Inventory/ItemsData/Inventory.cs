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

    public bool AddItem(Item i)
    {
        Debug.Log("Adding " + i.name);
        if (currentWeight + i.weight <= maxWeight)
        {
            items.Add(i);
            currentWeight += i.weight;
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

    public float CheckingCurrentWheight()
    {
        return currentWeight;
    }

    public bool Opens(int door)
    {
        foreach (Item i in items)
        {
            if(i is AccesItem)
            {
                AccesItem ai = (AccesItem) i;
                if (ai.Opens(door))
                {
                    return true;
                }
            }
        }

        return false;
    }

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
}
