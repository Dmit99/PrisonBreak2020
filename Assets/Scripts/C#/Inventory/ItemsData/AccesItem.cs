
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
    
    /// Boolean functien that calls if its true or false when using it on a door.
    public bool Opens(int door)
    {
        return doorId == door;
    }
}
