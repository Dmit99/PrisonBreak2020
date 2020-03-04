using UnityEngine;

public abstract class LandScape : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        GameManagerRandom.instance.regenerate.AddListener(Generate);
        Generate();
    }

    public virtual void Clean() { }

    public virtual void Generate() { }
}
