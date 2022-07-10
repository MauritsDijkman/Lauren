using UnityEngine;
using System;

public class Facebook : MonoBehaviour
{
    private GameObject HomeScreen;
    private PhoneHomeScreen homeScreenScript;
    private ObjectiveHandler objective;

    private GameObject StartScreenWM;
    private GameObject StartScreenWOM;
    private GameObject AlertScreen;
    private GameObject ProfileScreen;

    [NonSerialized] public bool playerShouldOpenProfile;
    [NonSerialized] public bool playerCheckedProfile;

    private void Awake()
    {
        HomeScreen = GameObject.Find("/Canvas/Phone/HomeScreen");
        homeScreenScript = FindObjectOfType<PhoneHomeScreen>();
        objective = FindObjectOfType<ObjectiveHandler>();

        StartScreenWM = GameObject.Find("/Canvas/Phone/FacebookScreen/StartScreenWithMessage");
        StartScreenWOM = GameObject.Find("/Canvas/Phone/FacebookScreen/StartScreenWithoutMessage");
        AlertScreen = GameObject.Find("/Canvas/Phone/FacebookScreen/AlertsScreen");
        ProfileScreen = GameObject.Find("/Canvas/Phone/FacebookScreen/ProfileScreen");
    }

    private void Start()
    {
        if (HomeScreen == null)
            throw new System.Exception("Add a HomeScreen UI to the phone.");
        if (homeScreenScript == null)
            throw new System.Exception("Add a PhoneHomeScreen script to the scene.");
        if (objective == null)
            throw new System.Exception("Add a ObjectiveHandler script to the scene.");

        if (StartScreenWM == null)
            throw new System.Exception("Add a StartScreen With Message to the FacebookScreen at the phone.");
        if (StartScreenWOM == null)
            throw new System.Exception("Add a StartScreen Without Message to the FacebookScreen at the phone.");
        if (AlertScreen == null)
            throw new System.Exception("Add a AlertsScreen UI to the FacebookScreen at the phone.");
        if (ProfileScreen == null)
            throw new System.Exception("Add a ProfileScreen UI to the FacebookScreen at the phone.");
    }

    public void OpenFacebook()
    {
        this.gameObject.SetActive(true);

        if (!playerCheckedProfile)
        {
            StartScreenWM.SetActive(true);
            StartScreenWOM.SetActive(false);
        }
        else
        {
            StartScreenWM.SetActive(false);
            StartScreenWOM.SetActive(true);
        }

        AlertScreen.SetActive(false);
        ProfileScreen.SetActive(false);
    }

    public void OpenHomePage()
    {
        if (!playerCheckedProfile)
        {
            if (!StartScreenWM.activeSelf)
                StartScreenWM.SetActive(true);
            if (StartScreenWOM.activeSelf)
                StartScreenWOM.SetActive(false);
        }
        else
        {
            if (StartScreenWM.activeSelf)
                StartScreenWM.SetActive(false);
            if (!StartScreenWOM.activeSelf)
                StartScreenWOM.SetActive(true);
        }

        if (AlertScreen.activeSelf)
            AlertScreen.SetActive(false);
    }

    public void OpenAlerts()
    {
        if (StartScreenWM.activeSelf)
            StartScreenWM.SetActive(false);
        if (StartScreenWOM.activeSelf)
            StartScreenWOM.SetActive(false);
        if (ProfileScreen.activeSelf)
            ProfileScreen.SetActive(false);
        if (!AlertScreen.activeSelf)
            AlertScreen.SetActive(true);
    }

    public void OpenProfile()
    {
        if (playerShouldOpenProfile)
        {
            if (AlertScreen.activeSelf)
                AlertScreen.SetActive(false);
            if (!ProfileScreen.activeSelf)
                ProfileScreen.SetActive(true);

            if (!playerCheckedProfile)
                objective.SetObjective("No objective at the moment");

            playerCheckedProfile = true;
        }
    }

    public void CloseApp()
    {
        if (StartScreenWM.activeSelf)
            StartScreenWM.SetActive(false);
        if (StartScreenWOM.activeSelf)
            StartScreenWOM.SetActive(false);
        if (AlertScreen.activeSelf)
            AlertScreen.SetActive(false);
        if (ProfileScreen.activeSelf)
            ProfileScreen.SetActive(false);

        this.gameObject.SetActive(false);

        HomeScreen.SetActive(true);
    }
}
