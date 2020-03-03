using UnityEngine;
using UnityEngine.UI;
using gamehelper;

public class GameManager : GameManagerHelper
{
    private RawImage qRcodeImage;
 
    #region Singleton...
    public static GameManager instance;
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

    protected override void Start()
    {
        qRcodeImage = GameObject.Find("WallWithQRCode").GetComponentInChildren<Canvas>().GetComponentInChildren<RawImage>();
        base.Start();

        /// Getting QR code image.
        StartCoroutine(GetPNG("https://chart.googleapis.com/chart?cht=qr&chs=300x300&chl=otpauth%3A%2F%2Ftotp%2FDmitri_The_Prisoner%3Fsecret%3DGM2DENRSG5BVSS2B%26issuer%3DPrisonBreak2020", qRcodeImage));

        /// Setting up and recieving every light in the prison.
        SetupAlarmLights();
    }

    protected override void Update()
    {
        base.Update();
    }
}
