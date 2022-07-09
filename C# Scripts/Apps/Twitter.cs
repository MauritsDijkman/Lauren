using UnityEngine;

public class Twitter : MonoBehaviour
{
    private GameObject HomeScreen;
    private PhoneHomeScreen homeScreenScript;


    private GameObject StartScreen;

    private void Awake()
    {
        HomeScreen = GameObject.Find("/Canvas/Phone/HomeScreen");
        homeScreenScript = FindObjectOfType<PhoneHomeScreen>();

        StartScreen = GameObject.Find("/Canvas/Phone/TwitterScreen/StartScreen");
    }

    private void Start()
    {
        if (HomeScreen == null)
            throw new System.Exception("Add a HomeScreen UI to the phone.");
        if (homeScreenScript == null)
            throw new System.Exception("Add a PhoneHomeScreen script to the scene.");

        if (StartScreen == null)
            throw new System.Exception("Add a StartScreen UI to Twitter on the phone.");
    }

    public void OpenTwitter()
    {
        this.gameObject.SetActive(true);
        StartScreen.SetActive(true);
    }

    public void CloseApp()
    {
        if (StartScreen.activeSelf)
            StartScreen.SetActive(false);

        this.gameObject.SetActive(false);

        HomeScreen.SetActive(true);
    }
}
