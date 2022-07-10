using UnityEngine;
using System;
using System.Collections;

public class TrafficLight : MonoBehaviour
{
    private GameManager gameManager;
    private DialogueSystem newDialogue;

    [SerializeField] private GameObject redLight;
    [SerializeField] private GameObject orangeLight;
    [SerializeField] private GameObject greenLight;

    private bool hasStarted = false;
    private bool hasFinished = true;

    private float secondsForGreen = 10f;
    private float secondsForOrange = 5f;
    private float secondsForRed = 2f;

    [NonSerialized] public bool greenLightIsActive = false;
    [NonSerialized] public bool orangeLightIsActive = false;
    [NonSerialized] public bool redLightIsActive = false;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        newDialogue = FindObjectOfType<DialogueSystem>();
    }

    private void Start()
    {
        if (gameManager == null)
            throw new System.Exception("Add a GameManager script to the scene.");
        if (newDialogue == null)
            throw new System.Exception("Add a DialogueSystem script to the scene.");
        if (redLight == null)
            throw new System.Exception("Drag a red light to the traffic light script.");
        if (orangeLight == null)
            throw new System.Exception("Drag a orange light to the traffic light script.");
        if (greenLight == null)
            throw new System.Exception("Drag a green light to the traffic light script.");

        redLight.SetActive(false);
        orangeLight.SetActive(false);
        greenLight.SetActive(false);
    }

    private void Update()
    {
        // If hasStarted is false and hasFinished is true
        if (!hasStarted && hasFinished)
            StartCoroutine(HandleTraficLights());
    }

    private IEnumerator HandleTraficLights()
    {
        hasStarted = true;
        hasFinished = false;

        secondsForGreen = UnityEngine.Random.Range(5f, 10f);
        secondsForOrange = UnityEngine.Random.Range(4f, 9f);
        secondsForRed = UnityEngine.Random.Range(2f, 4f);

        greenLightIsActive = true;
        redLightIsActive = false;

        // Turns on the green light
        greenLight.SetActive(true);
        orangeLight.SetActive(false);
        redLight.SetActive(false);

        yield return new WaitForSeconds(secondsForOrange);

        greenLightIsActive = false;
        orangeLightIsActive = true;

        // Turns on the orange light
        greenLight.SetActive(false);
        orangeLight.SetActive(true);
        redLight.SetActive(false);

        yield return new WaitForSeconds(secondsForRed);

        orangeLightIsActive = false;
        redLightIsActive = true;

        // Turns on the red light
        greenLight.SetActive(false);
        orangeLight.SetActive(false);
        redLight.SetActive(true);

        yield return new WaitForSeconds(secondsForGreen);

        hasStarted = false;
        hasFinished = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            if (orangeLightIsActive)
            {
                newDialogue.SetYellowLightState();
                return;
            }
            if (redLightIsActive)
            {
                newDialogue.SetRedLightState();
                return;
            }
        }
    }
}
