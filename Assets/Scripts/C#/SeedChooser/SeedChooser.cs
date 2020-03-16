using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SeedChooser : MonoBehaviour
{
    [SerializeField] private Text inputText;
    private int seedNumber;
    #region Singleton...
    public static SeedChooser instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != null)
        {
            Destroy(this);
        }
    }
    #endregion

    public void SaveSeedNumberGiven()
    {
        if(inputText.text != " ")
        {
            StartCoroutine(SaveAndNextScene());
        }
    }

    IEnumerator SaveAndNextScene()
    {
        int.TryParse(inputText.text, out seedNumber);
        PlayerPrefs.SetInt(key: "SeedNumber", value: seedNumber);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
