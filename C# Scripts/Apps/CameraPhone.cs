using UnityEngine;
using System;

public class CameraPhone : MonoBehaviour
{
    private GameObject HomeScreen;
    private PhoneHomeScreen homeScreenScript;
    private ObjectiveHandler objective;

    private GameObject StartScreenUI;
    private GameObject PictureTakenUI;

    [NonSerialized] public bool playerShouldMakeSelfie;
    [NonSerialized] public bool playerMadeSelfie;

    private void Awake()
    {
        HomeScreen = GameObject.Find("/Canvas/Phone/HomeScreen");
        homeScreenScript = FindObjectOfType<PhoneHomeScreen>();
        objective = FindObjectOfType<ObjectiveHandler>();

        StartScreenUI = GameObject.Find("/Canvas/Phone/CameraScreen/StartScreen");
        PictureTakenUI = GameObject.Find("/Canvas/Phone/CameraScreen/PictureTakenScreen");
    }

    private void Start()
    {
        if (HomeScreen == null)
            throw new System.Exception("Add a HomeScreen UI to the phone.");
        if (homeScreenScript == null)
            throw new System.Exception("Add a PhoneHomeScreen script to the scene.");
        if (objective == null)
            throw new System.Exception("Add a ObjectiveHandler script to the scene.");

        if (StartScreenUI == null)
            throw new System.Exception("Add a StartScreen UI to the Camera at the phone.");
        if (PictureTakenUI == null)
            throw new System.Exception("Add a PictureTaken UI to the Camera at the phone.");
    }

    public void OpenCamera()
    {
        this.gameObject.SetActive(true);

        if (!playerMadeSelfie)
        {
            StartScreenUI.SetActive(true);
            PictureTakenUI.SetActive(false);
        }
        else
        {
            StartScreenUI.SetActive(false);
            PictureTakenUI.SetActive(true);
        }
    }

    public void MakeSelfie()
    {
        if (playerShouldMakeSelfie)
        {
            StartScreenUI.SetActive(false);
            PictureTakenUI.SetActive(true);

            if (!playerMadeSelfie)
                objective.SetObjective("No objective at the moment");

            playerMadeSelfie = true;
        }
    }

    public void CloseApp()
    {
        if (StartScreenUI.activeSelf)
            StartScreenUI.SetActive(false);
        if (PictureTakenUI.activeSelf)
            PictureTakenUI.SetActive(false);

        this.gameObject.SetActive(false);

        HomeScreen.SetActive(true);
    }
}
