using UnityEngine;
using System;

public class PhoneControler : MonoBehaviour
{
    private Animator animator;
    private GameObject phoneModelCar;
    private PhoneHomeScreen homescreen;
    private BlurController blurController;

    [NonSerialized] public bool phoneIsOpen = false;

    private GameObject homeScreenUI;
    private GameObject WhatsAppScreen;
    private GameObject InstagramScreen;
    private GameObject TwitterScreen;
    private GameObject FacebookScreen;
    private GameObject CameraScreen;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        phoneModelCar = GameObject.Find("/Car/Phone");
        homescreen = FindObjectOfType<PhoneHomeScreen>();
        blurController = FindObjectOfType<BlurController>();

        homeScreenUI = GameObject.Find("/Canvas/Phone/HomeScreen");
        WhatsAppScreen = GameObject.Find("/Canvas/Phone/WhatsAppScreen");
        InstagramScreen = GameObject.Find("/Canvas/Phone/InstagramScreen");
        TwitterScreen = GameObject.Find("/Canvas/Phone/TwitterScreen");
        FacebookScreen = GameObject.Find("/Canvas/Phone/FacebookScreen");
        CameraScreen = GameObject.Find("/Canvas/Phone/CameraScreen");
    }

    private void Start()
    {
        if (animator == null)
            throw new System.Exception("Add an animator to the phone.");
        if (phoneModelCar == null)
            throw new System.Exception("Add a phone model to the scene.");
        if (homescreen == null)
            throw new System.Exception("Add a Phone Home Screen script to the scene.");
        if (blurController == null)
            throw new System.Exception("Add a BlurController script to the scene.");
        if (homeScreenUI == null)
            throw new System.Exception("Add a HomeScreen to the Phone on the Canvas.");
        if (WhatsAppScreen == null)
            throw new System.Exception("Add a WhatsAppScreen to the Phone on the Canvas.");
        if (InstagramScreen == null)
            throw new System.Exception("Add a InstagramScreen to the Phone on the Canvas.");
        if (TwitterScreen == null)
            throw new System.Exception("Add a TwitterScreen to the Phone on the Canvas.");
        if (FacebookScreen == null)
            throw new System.Exception("Add a FacebookScreen to the Phone on the Canvas.");
        if (CameraScreen == null)
            throw new System.Exception("Add a CameraScreen to the Phone on the Canvas.");

        CloseAllScreens();
    }

    public void OpenPhone()
    {
        blurController.TurnBlurOn();

        animator.SetBool("PhoneIsOpen", true);

        phoneModelCar.SetActive(false);
        homeScreenUI.SetActive(true);

        phoneIsOpen = true;
    }

    public void ClosePhone()
    {
        CloseAllScreens();

        blurController.TurnBlurOff();

        animator.SetBool("PhoneIsOpen", false);

        phoneModelCar.SetActive(true);

        phoneIsOpen = false;
    }

    private void CloseAllScreens()
    {
        if (homeScreenUI.activeSelf)
            homeScreenUI.SetActive(false);
        if (WhatsAppScreen.activeSelf)
            WhatsAppScreen.SetActive(false);
        if (InstagramScreen.activeSelf)
            InstagramScreen.SetActive(false);
        if (TwitterScreen.activeSelf)
            TwitterScreen.SetActive(false);
        if (FacebookScreen.activeSelf)
            FacebookScreen.SetActive(false);
        if (CameraScreen.activeSelf)
            CameraScreen.SetActive(false);
    }
}
