public class Rock : Pickup
{
    /// The visual part of the rock.
    protected override Item CreateItem()
    {
        return new ThrowableItem(name: name, weight: weight);
    }
}
