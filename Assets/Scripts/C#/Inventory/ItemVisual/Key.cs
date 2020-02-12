public class Key : Pickup
{
    public int doorID;

    /// The visual part of the key.

    protected override Item CreateItem()
    {
        return new AccesItem(name: _name, weight: weight, doorId: doorID);
    }
}
