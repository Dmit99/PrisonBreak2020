public class BonusItem : Item
{
    /// Properties.
    public float points;


    /// Constructor.
    public BonusItem(string name, float weight, float points) : base(name, weight)
    {
        this.points = points;
    }

    /// Methods.
}
