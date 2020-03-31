using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameInformation;
    [SerializeField] private TextMeshProUGUI totalPartsFoundInformation;
    private bool enoughParts = false;
    #region singleton
    public static UIManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != null)
        {
            Destroy(obj: this);
        }
    }
    #endregion

    private void Update()
    {
        if (enoughParts)
        {
            EnoughPartsChecker();
        }
    }

    public void StartUISetup()
    {
        gameInformation.text = "";
        totalPartsFoundInformation.text = "You found 0 parts out of 3 to escape the island.";
    }

    public TextMeshProUGUI UpdateUIGameInformation(string informationText)
    {
        gameInformation.text = informationText;
        return gameInformation;
    }

    public TextMeshProUGUI UpdateUITotalPartsInformation(int addedValue)
    {
        if (GameManagerRandom.instance.GetPartsNumber() < 3)
        {
            if(GameManagerRandom.instance.GetPartsNumber() == 2)
            {
                enoughParts = true;
                return null;
            }
            if (GameManagerRandom.instance.GetPartsNumber() == 0)
            {
                GameManagerRandom.instance.AddPartsNumber(addedValue);
                totalPartsFoundInformation.text = $"You found {GameManagerRandom.instance.GetPartsNumber()} part out of 3 to escape the island.";
                return totalPartsFoundInformation;
            }
            GameManagerRandom.instance.AddPartsNumber(addedValue);
            totalPartsFoundInformation.text = $"You found {GameManagerRandom.instance.GetPartsNumber()} parts out of 3 to escape the island.";
            return totalPartsFoundInformation;
        }
        return null;
    }

    public TextMeshProUGUI EnoughPartsChecker()
    {
        totalPartsFoundInformation.text = "You have found every part thats needed to escape the island!\nPress escape to escape!";
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            totalPartsFoundInformation.text = "";
            gameInformation.text = "";
            StartCoroutine(GameManagerRandom.instance.StartEndScene());
        }
        return totalPartsFoundInformation;
    }
}
