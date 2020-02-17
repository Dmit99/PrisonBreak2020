using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class KeyPadScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttons = new List<GameObject>();
    private int buttonsAvailable;

    public KeyPadScript()
    {

    }

    /// <summary>
    /// Starting up the keypad numbers by adding the numbers in the list and update them.
    /// </summary>
    public void Starting()
    {
        /// buttonsAvailable is the amount of buttons on the keypad. This must be hardcoded because the game has to find the buttons first and needs to know how many buttons there are.
        buttonsAvailable = 10;
        
        for (int i = 0; i < buttonsAvailable; i++)
        {
            buttons.Add(GameObject.Find(i.ToString()));
        }

        UpdateTextOnButtons();
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
}
