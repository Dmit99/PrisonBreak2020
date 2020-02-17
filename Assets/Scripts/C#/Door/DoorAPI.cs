using UnityEngine;

public class DoorAPI : JsonNetwork
{
    #region GoogleAuthenticator stuff
    private string GoogleAuthNumber;
    public bool GoogleEnterPressed;
    #endregion

    #region Door stuff
    private GameObject door;
    private float initialRotation;
    #endregion


    public override void Start()
    {
        initialRotation = transform.rotation.eulerAngles.y;
        door = gameObject;
    }

    private void Update()
    {
        /// If the authenticator number has been pressed and the boolean is true...
        if(GoogleAuthNumber != null && GoogleEnterPressed)
        {
            StartCoroutine(GetRequest("https://www.authenticatorapi.com/Validate.aspx?Pin=" + GoogleAuthNumber + "& SecretCode= 342627CYKA"));
            GoogleEnterPressed = false;
        }
    }

    /// When the Json is parsed it will tryparse the string in a true or false and that will be recieved in the OpenDoorMethod.
    protected override void ParseJSON(string jsonString)
    {
        OpenDoor(bool.TryParse(jsonString, out _));
    }


    /// Rotates the door to "Open" with the result recieved by another method.
    private void OpenDoor(bool open)
    {
        if (open && transform.rotation.eulerAngles.y < initialRotation + 80)
        {
            door.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, initialRotation + 80, 0), 5);
        }
        else if (!open && transform.rotation.eulerAngles.y > initialRotation)
        {
            door.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, initialRotation, 0), 5);
        }
    }
}
