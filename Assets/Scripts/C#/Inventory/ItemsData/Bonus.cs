using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : Pickup
{
    public int points;

    protected override Item CreateItem()
    {
        return new BonusItem(name, weight, points);
    }
}
