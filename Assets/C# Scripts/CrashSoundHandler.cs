using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashSoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource crashSound;
    [SerializeField] private AudioSource lastDialogue;

    private float timer;
    private bool dialogueHasPlayed;

    private void Start()
    {
        if (crashSound == null)
            throw new System.Exception("Drag a CrashSound AudioSource to the script.");
        if (lastDialogue == null)
            throw new System.Exception("Drag a LastDialogue AudioSource to the script.");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        timer = 65;
        dialogueHasPlayed = false;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (!crashSound.isPlaying && !dialogueHasPlayed)
        {
            lastDialogue.Play();
            dialogueHasPlayed = true;
        }

        if (timer <= 0f)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
