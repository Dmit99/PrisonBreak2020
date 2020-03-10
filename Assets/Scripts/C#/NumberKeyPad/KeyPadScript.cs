using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class KeyPadScript : JsonNetwork
{
    [SerializeField] private List<GameObject> buttons = new List<GameObject>();
    private int buttonsAvailable;
    private TextMeshProUGUI screenDisplay;
    private string oldNumber;
    private string googleAuthNumber;
    DoorAPI secondDoor;

    public KeyPadScript()
    {

    }

    /// <summary>
    /// Starting up the keypad numbers by adding the numbers in the list and update them.
    /// </summary>
    public void Starting()
    {
        oldNumber = "";
        googleAuthNumber = "";
        secondDoor = GameObject.Find("Door2(API)").GetComponent<DoorAPI>();

        /// buttonsAvailable is the amount of buttons on the keypad. This must be hardcoded because the game has to find the buttons first and needs to know how many buttons there are.
        buttonsAvailable = 10;
        for (int i = 0; i < buttonsAvailable; i++)
        {
            buttons.Add(GameObject.Find(i.ToString()));
        }

        UpdateTextOnButtons();

        /// Finding the screen.
        screenDisplay = GameObject.Find("Screen").GetComponentInChildren<TextMeshProUGUI>();
        screenDisplay.text = "";
    }

    /// <summary>
    /// This method will automaticly update the Text on the buttons.
    /// Buttons will be randomized in order.
    /// </summary>
    public void ChangeNumberLayout()
    {
        /// Buttons will be randomized in order.
        buttons = buttons.OrderBy(i => Random.value).ToList();

        /// For every button in the order of the buttons. (So because of the random numbers the list of buttons is like: 3,6,2,0... instead of 1,2,3,4....)
        /// The first number thats in this explaination is 3. 3 is the first number in the list. So the gameobject name will be 3 of the first item in the list.
        /// This is also available in the inspector to see.
        
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].name = i.ToString();
        }

        UpdateTextOnButtons();
    }

    public void UpdateTextOnButtons()
    {
        /// The visual text on the buttons will have the same numbers on them as the name of the item.
        foreach (GameObject item in buttons)
        {
            item.GetComponentInChildren<TextMeshProUGUI>().text = item.name;
        }
    }


    public void InsertNumber(string recievedNumber)
    {
        oldNumber += recievedNumber;
        UpdateScreenDisplay(oldNumber);
    }

    private void UpdateScreenDisplay(string insertedNumber)
    {
        screenDisplay.text = insertedNumber;
    }

    /// <summary>
    /// Checking if the number is the same number as on your phone.
    /// </summary>
    public void NumberCorrectChecker()
    {
        googleAuthNumber = oldNumber;
        if (googleAuthNumber != "")
        {
           StartCoroutine(GetRequest("https://www.authenticatorapi.com/Validate.aspx?Pin=" + googleAuthNumber + "&SecretCode=342627CYKA"));
        }
        oldNumber = "";
        UpdateScreenDisplay(oldNumber);
    }


    /// When the Json is parsed it will tryparse the string in a true or false and that will be recieved in the OpenDoorMethod.
    protected override void ParseJSON(string jsonString)
    {
        secondDoor.openDoor = jsonString;
    }


}
