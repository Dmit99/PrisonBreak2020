using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Pickup
{
    /// The visual part of the rock.

    public override void Start()
    {
        rock = new ThrowableItem(name: name, weight: weight);
        _name = "ThrowableRock";
        item = this.gameObject;
    }

    public void AddRock() 
    { 
        AddToInventory(rock);
    }
}
