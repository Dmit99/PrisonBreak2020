using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SeedChooser : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playbutton;
    [SerializeField] private Text inputText;
    private int seedNumber;
    private FontStyles noBold = FontStyles.Bold - 1;
    private FontStyles bold = FontStyles.Bold;
    private Color green = new Color32(17, 212, 33, 255);
    //private Color yellow = new Color32(238, 241, 92, 255);
    private Color red = new Color32(212, 25, 17, 255);

    private void Update()
    {
        if(inputText.text == "")
        {
            playbutton.faceColor = red;
            playbutton.fontStyle = noBold;
        }

        if(inputText.text != "")
        {
            playbutton.faceColor = green;
            playbutton.fontStyle = bold; 
        }
    }

    public void SaveSeedNumberGiven()
    {
        if(inputText.text != "")
        {
            StartCoroutine(Save());
        }
        else if(inputText.text == "")
        {
            return;
        }
    }

    IEnumerator Save()
    {
        int.TryParse(inputText.text, out seedNumber);
        PlayerPrefs.SetInt(key: "SeedNumber", value: seedNumber);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
