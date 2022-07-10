using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TMP_Text percentageText;
    [SerializeField] private AudioMixer audioMixer;

    private void Start()
    {
        if (!mainMenu.activeSelf)
            mainMenu.SetActive(true);
        if (optionsMenu.activeSelf)
            optionsMenu.SetActive(false);
        if (creditsMenu.activeSelf)
            creditsMenu.SetActive(false);
        if (loadingScreen.activeSelf)
            loadingScreen.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelAsync());
    }

    private IEnumerator LoadLevelAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Level");

        if (mainMenu.activeSelf)
            mainMenu.SetActive(false);
        if (!loadingScreen.activeSelf)
            loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            loadingSlider.value = progress;
            percentageText.text = progress * 100f + "%";

            yield return null;
        }
    }

    public void OpenOptions()
    {
        if (mainMenu.activeSelf)
            mainMenu.SetActive(false);
        if (!optionsMenu.activeSelf)
            optionsMenu.SetActive(true);
    }

    public void OpenCampaign()
    {
        Application.OpenURL("https://www.adcouncil.org/campaign/texting-and-driving-prevention");
    }

    public void OpenCredits()
    {
        if (mainMenu.activeSelf)
            mainMenu.SetActive(false);
        if (!creditsMenu.activeSelf)
            creditsMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        if (!mainMenu.activeSelf)
            mainMenu.SetActive(true);
        if (optionsMenu.activeSelf)
            optionsMenu.SetActive(false);
        if (creditsMenu.activeSelf)
            creditsMenu.SetActive(false);
    }

    public void SetVolume(float pVolume)
    {
        audioMixer.SetFloat("volume", pVolume);
    }

    public void SetFullScreen(bool pIsFullScreen)
    {
        Screen.fullScreen = pIsFullScreen;
    }
}
