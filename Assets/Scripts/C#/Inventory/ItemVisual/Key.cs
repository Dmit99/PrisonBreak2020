using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Pickup
{
    public int doorID;

    /// The visual part of the key.

    public override void Start()
    {
        key = new AccesItem(name: _name, weight: weight, doorId: doorID);
    }

    public void AddKey()
    {
        AddToInventory(key);
    }
}
