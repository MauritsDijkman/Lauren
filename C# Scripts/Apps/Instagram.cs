using UnityEngine;
using System;

public class Instagram : MonoBehaviour
{
    private GameObject HomeScreen;
    private PhoneHomeScreen homeScreenScript;
    private ObjectiveHandler objective;

    private GameObject StartScreen;
    private GameObject StoryScreen;
    private GameObject MessagesScreen;
    private GameObject ChatScreen;

    [NonSerialized] public bool playerShouldOpenStory;
    [NonSerialized] public bool playerCheckedStory;

    [NonSerialized] public bool playerShouldOpenMeme;
    [NonSerialized] public bool playerCheckedMeme;

    private void Awake()
    {
        HomeScreen = GameObject.Find("/Canvas/Phone/HomeScreen");
        homeScreenScript = FindObjectOfType<PhoneHomeScreen>();
        objective = FindObjectOfType<ObjectiveHandler>();

        StartScreen = GameObject.Find("/Canvas/Phone/InstagramScreen/StartScreen");
        StoryScreen = GameObject.Find("/Canvas/Phone/InstagramScreen/StoryScreen");
        MessagesScreen = GameObject.Find("/Canvas/Phone/InstagramScreen/MessagesScreen");
        ChatScreen = GameObject.Find("/Canvas/Phone/InstagramScreen/ChatScreen");
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
            throw new System.Exception("Add a StartScreens UI to Instagram on the phone.");
        if (StoryScreen == null)
            throw new System.Exception("Add a StoryScreen UI to Instagram on the phone.");
        if (MessagesScreen == null)
            throw new System.Exception("Add a MessagesScreen UI to Instagram on the phone.");
        if (ChatScreen == null)
            throw new System.Exception("Add a ChatScreen UI to Instagram on the phone.");
    }

    public void OpenInstagram()
    {
        this.gameObject.SetActive(true);

        StartScreen.SetActive(true);
        StoryScreen.SetActive(false);
        MessagesScreen.SetActive(false);
        ChatScreen.SetActive(false);
    }

    public void OpenStory()
    {
        if (playerShouldOpenStory)
        {
            StartScreen.SetActive(false);
            StoryScreen.SetActive(true);

            if (!playerCheckedStory)
                objective.SetObjective("No objective at the moment");

            playerCheckedStory = true;
        }
    }

    public void OpenMessages()
    {
        StartScreen.SetActive(false);
        MessagesScreen.SetActive(true);
    }

    public void OpenChat()
    {
        if (playerShouldOpenMeme)
        {
            MessagesScreen.SetActive(false);
            ChatScreen.SetActive(true);

            if (!playerCheckedMeme)
                objective.SetObjective("No objective at the moment");

            playerCheckedMeme = true;
        }
    }

    public void BackToHomePage()
    {
        if (MessagesScreen.activeSelf)
            MessagesScreen.SetActive(false);
        if (StoryScreen.activeSelf)
            StoryScreen.SetActive(false);

        StartScreen.SetActive(true);
    }

    public void BackToChatsPage()
    {
        ChatScreen.SetActive(false);
        MessagesScreen.SetActive(true);
    }

    public void CloseApp()
    {
        if (StartScreen.activeSelf)
            StartScreen.SetActive(false);
        if (StoryScreen.activeSelf)
            StoryScreen.SetActive(false);
        if (MessagesScreen.activeSelf)
            MessagesScreen.SetActive(false);
        if (ChatScreen.activeSelf)
            ChatScreen.SetActive(false);

        this.gameObject.SetActive(false);

        HomeScreen.SetActive(true);
    }
}
