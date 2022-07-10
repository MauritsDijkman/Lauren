using UnityEngine;
using System;

public class MouseLook : MonoBehaviour
{
    private GameManager gameManager;
    private PhoneControler phone;

    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;
    [SerializeField] private float maxRotation = 50f;

    private float mouseX;
    private float mouseY;

    private float xRotation = 0f;

    [NonSerialized] public float yRotation = 0f;

    private Quaternion originalRotation;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        phone = FindObjectOfType<PhoneControler>();
    }

    private void Start()
    {
        if (gameManager == null)
            throw new System.Exception("Add a gamemanager to the scene.");
        if (phone == null)
            throw new System.Exception("Add a PhoneController script to the scene.");

        originalRotation = playerBody.rotation;
    }

    private void Update()
    {
        if (gameManager.IsInputEnabled)
            HandleCamera();
    }

    private void HandleCamera()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxRotation, maxRotation);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -maxRotation, maxRotation);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0f);

        playerBody.transform.localRotation = Quaternion.Euler(0, yRotation, 0) * originalRotation;
    }
}
