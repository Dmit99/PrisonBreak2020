using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private PostProcessVolume activeVolume;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeVolume != null)
        {
            Vignette vignette;
            activeVolume.profile.TryGetSettings(out vignette);
            if(vignette != null)
            {
                vignette.intensity.value = 0.675f;
            }
        }
    }
}
