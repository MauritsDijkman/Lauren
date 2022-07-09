using UnityEngine;
using System;

public class WhatsApp : MonoBehaviour
{
    private GameObject HomeScreen;
    private PhoneHomeScreen homeScreenScript;
    private ObjectiveHandler objective;

    private GameObject StartScreen;
    private GameObject ChatNotSend;
    private GameObject ChatSend;

    private bool messageHasBeenSend = false;

    [NonSerialized] public bool playerShouldSendSelfie;
    [NonSerialized] public bool playerHasSendSelfie;

    private void Awake()
    {
        HomeScreen = GameObject.Find("/Canvas/Phone/HomeScreen");
        homeScreenScript = FindObjectOfType<PhoneHomeScreen>();
        objective = FindObjectOfType<ObjectiveHandler>();

        StartScreen = GameObject.Find("/Canvas/Phone/WhatsAppScreen/StartScreen");
        ChatNotSend = GameObject.Find("/Canvas/Phone/WhatsAppScreen/Chat (not send)");
        ChatSend = GameObject.Find("/Canvas/Phone/WhatsAppScreen/Chat (send)");
    }

    private void Start()
    {
        if (HomeScreen == null)
            throw new System.Exception("Add a HomeScreen UI to the phone.");
        if (homeScreenScript == null)
            throw new System.Exception("Add a PhoneHomeScreen script to the scene.");
        if (objective == null)
            throw new System.Exception("Add a ObjectiveHandler script to the scene.");

        if (StartScreen == null)
            throw new System.Exception("Add a StartScreen UI to WhatsApp on the phone.");
        if (ChatNotSend == null)
            throw new System.Exception("Add a ChatNotSend UI to WhatsApp on the phone.");
        if (ChatSend == null)
            throw new System.Exception("Add a ChatSend UI to WhatsApp on the phone.");
    }

    public void OpenWhatsApp()
    {
        this.gameObject.SetActive(true);

        StartScreen.SetActive(true);
        ChatNotSend.SetActive(false);
        ChatSend.SetActive(false);
    }

    public void OpenChat()
    {
        if (playerShouldSendSelfie)
        {
            if (messageHasBeenSend)
                ChatSend.SetActive(true);
            else
                ChatNotSend.SetActive(true);

            StartScreen.SetActive(false);
        }
    }

    public void SendMessage()
    {
        ChatNotSend.SetActive(false);
        ChatSend.SetActive(true);

        messageHasBeenSend = true;

        if (!playerHasSendSelfie)
            objective.SetObjective("No objective at the moment");

        playerHasSendSelfie = true;
    }

    public void BackToChats()
    {
        if (ChatNotSend.activeSelf)
            ChatNotSend.SetActive(false);
        if (ChatSend.activeSelf)
            ChatSend.SetActive(false);

        StartScreen.SetActive(true);
    }

    public void CloseApp()
    {
        if (StartScreen.activeSelf)
            StartScreen.SetActive(false);
        if (ChatNotSend.activeSelf)
            ChatNotSend.SetActive(false);
        if (ChatSend.activeSelf)
            ChatSend.SetActive(false);

        this.gameObject.SetActive(false);

        HomeScreen.SetActive(true);
    }
}
