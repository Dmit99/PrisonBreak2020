using UnityEngine;

public abstract class LandScape : MonoBehaviour
{

    private void Start()
    {
        Initialize();
    }
    // Start is called before the first frame update
    public void Initialize()
    {
        GameManagerRandom.instance.regenerate.AddListener(Generate);
        Generate();
    }

    public virtual void Clean() { }

    public virtual void Generate() { }
}
