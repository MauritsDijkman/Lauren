using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private PhoneControler phone;

    [SerializeField] private GameObject pauseScreen;
    [NonSerialized] public bool IsInputEnabled;

    private bool pauseScreenIsActive = false;

    private void Awake()
    {
        phone = FindObjectOfType<PhoneControler>();

        IsInputEnabled = true;
    }

    private void Start()
    {
        if (phone == null)
            throw new System.Exception("Add a PhoneController script to the scene.");
        if (pauseScreen == null)
            throw new System.Exception("Add a PauseScreen to the script.");

        if (pauseScreen.activeSelf)
            pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (phone.phoneIsOpen || pauseScreen.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (pauseScreen.activeSelf)
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
            IsInputEnabled = false;
        }
        else
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            IsInputEnabled = true;
        }

        if (Input.GetButtonDown("Pause"))
        {
            pauseScreenIsActive ^= true;
            HandlePauseScreen(pauseScreenIsActive);
        }
    }

    private void HandlePauseScreen(bool pIsActive)
    {
        pauseScreen.SetActive(pIsActive);
    }
}
