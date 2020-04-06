using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private PostProcessVolume activeVolume;
    private float lerp = 0;
    private const float duration = 1.5f;
    private const float maxValue = 50;
    private const float minValue = 20;

    private Bloom bloom;
    private bool startLerping = false;
    private bool change = false;

    // Start is called before the first frame update
    void Start()
    {

        /// Getting active post processing.
        /// If there is none. Lerping will not be used!
        if (activeVolume != null)
        {
            activeVolume.profile.TryGetSettings(out bloom);
            if (bloom != null)
            {
                startLerping = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startLerping)
        {
            StartLerpingBloomEffect();
        }
    }

    void StartLerpingBloomEffect()
    {
        if (!change)
        {
            lerp += Time.deltaTime / duration;
            bloom.intensity.value = Mathf.Lerp(a: minValue, b: maxValue, t: lerp);
            if(bloom.intensity.value == 50)
            {
                lerp = 0;
                change = true;
            }
        }
        
        if (change)
        {
            lerp += Time.deltaTime / duration;
            bloom.intensity.value = Mathf.Lerp(a: maxValue, b: minValue, t: lerp);
            if(bloom.intensity.value == 20)
            {
                lerp = 0;
                change = false;
            }
        }
    }
}
