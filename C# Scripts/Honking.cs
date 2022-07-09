using UnityEngine;
using System;

public class Honking : MonoBehaviour
{
    private GameManager gameManager;
    private MoveToLocation car;
    private TrafficLight trafficLight;

    [SerializeField] private AudioClip[] audio;
    [SerializeField] private AudioSource randomSound;
    [SerializeField] private float totalTime = 5f;

    private float timer;

    [NonSerialized] public bool carInFrontYORTrafficLight = false;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        car = FindObjectOfType<MoveToLocation>();
        trafficLight = FindObjectOfType<TrafficLight>();
    }

    private void Start()
    {
        timer = totalTime;

        if (gameManager == null)
            throw new System.Exception("Add a GameManager script to the scene.");
        if (car == null)
            throw new System.Exception("Add a MoveToLocation script to the scene");
        if (trafficLight == null)
            throw new System.Exception("Add a TrafficLight script to the scene");
        if (randomSound == null)
            throw new System.Exception("Add a AudioSource to the GameObject");
    }

    private void Update()
    {
        if (gameManager.IsInputEnabled)
        {
            if (car.movingSpeed == 0 && !carInFrontYORTrafficLight)
                timer -= Time.deltaTime;
            else
            {
                timer = totalTime;

                if (randomSound.isPlaying)
                    randomSound.Stop();
            }

            if (timer <= 0f)
                PlayRandomSound();
        }
    }

    private void PlayRandomSound()
    {
        if (!randomSound.isPlaying)
        {
            randomSound.clip = audio[UnityEngine.Random.Range(0, audio.Length)];
            randomSound.Play();
        }
    }
}
