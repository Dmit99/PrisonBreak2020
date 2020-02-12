using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    private void Start()
    {
        Item beachBall = new BonusItem(name: "beachBall", weight: 4f, points: 20);
        Debug.Log("Item is " + beachBall.name + " and wheights " + beachBall.weight);

        if(beachBall is BonusItem)
        {
            Debug.Log(message: "beachball is a bonus item.");
        }

        if(beachBall is AccesItem)
        {
            Debug.Log(message: "beachball is a acces item.");
        }


        Item key = new AccesItem(name: "Key of Doom", weight: 1f, doorId: 1);
        Debug.Log("Item is " + key.name + " and wheights " + key.weight);



        /// Here we transform the item into a bonusitem.
        BonusItem b = (BonusItem)beachBall;
        Debug.Log(b.name + " gives you " + b.points + " points");

        AccesItem k = (AccesItem)key;
        Debug.Log(k.name + " opens door number: " + k.doorId);

        if (Inventory.instance.AddItem(beachBall))
        {
            Debug.Log("Inventory has added the item " + Inventory.instance.Count() + " to the list.");
        }

        if (Inventory.instance.AddItem(key))
        {
            Debug.Log("Inventory has added " + Inventory.instance.Count() + " to the list.");
        }

        if (Inventory.instance.Opens(door: 1))
        {
            Debug.Log("I can open the door with this key.");
        }
        else
        {
            Debug.Log("I can't open the door with this.");
        }

        if(Inventory.instance.Opens(door: 2))
        {
            Debug.Log("I can open the door with this key.");
        }
        else
        {
            Debug.Log("I can't open the door with this.");
        }
    }
}
