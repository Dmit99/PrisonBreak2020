using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccesItem : Item
{
    /// Properties
    public int doorId;


    /// Constructor
    public AccesItem(string name, float weight, int doorId) : base(name, weight)
    {
        this.doorId = doorId;
    }

    /// Methods
}
