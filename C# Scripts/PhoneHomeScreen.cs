using UnityEngine;
using System;

public class PhoneHomeScreen : MonoBehaviour
{
    private ObjectiveHandler objective;

    private WhatsApp whatsApp;
    private Instagram instagram;
    private Twitter twitter;
    private Facebook facebook;
    private CameraPhone camera;

    [NonSerialized] public bool playerShouldOpenChirpy;
    [NonSerialized] public bool playerOpenendChirpy;

    private void Awake()
    {
        objective = FindObjectOfType<ObjectiveHandler>();

        whatsApp = FindObjectOfType<WhatsApp>();
        instagram = FindObjectOfType<Instagram>();
        twitter = FindObjectOfType<Twitter>();
        facebook = FindObjectOfType<Facebook>();
        camera = FindObjectOfType<CameraPhone>();
    }

    private void Start()
    {
        if (objective == null)
            throw new System.Exception("Add a ObjectiveHandler script to the scene.");

        if (whatsApp == null)
            throw new System.Exception("Add a WhatsApp script to the scene.");
        if (instagram == null)
            throw new System.Exception("Add an Instagram script to the scene.");
        if (twitter == null)
            throw new System.Exception("Add a Twitter script to the scene.");
        if (facebook == null)
            throw new System.Exception("Add a Facebook script to the scene.");
        if (camera == null)
            throw new System.Exception("Add a Facebook script to the scene.");
    }

    public void OpenWhatsApp()
    {
        this.gameObject.SetActive(false);
        whatsApp.OpenWhatsApp();
    }

    public void OpenInstagram()
    {
        this.gameObject.SetActive(false);
        instagram.OpenInstagram();
    }

    public void OpenTwitter()
    {
        if (playerShouldOpenChirpy)
        {
            this.gameObject.SetActive(false);
            twitter.OpenTwitter();

            if (!playerOpenendChirpy)
                objective.SetObjective("No objective at the moment");

            playerOpenendChirpy = true;
        }
    }

    public void OpenFacebook()
    {
        this.gameObject.SetActive(false);
        facebook.OpenFacebook();
    }

    public void OpenCamera()
    {
        camera.OpenCamera();
    }
}
