using UnityEngine;
using System;

public class RayCast : MonoBehaviour
{
    private GameManager gameManager;
    [NonSerialized] public RaycastHit hitInfo;
    private PhoneControler phone;
    private GameObject interactionUI;
    private GameObject grabUI;
    private int layerMask;
    private bool EPressed = false;

    [SerializeField] private Camera playerCamera;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        phone = FindObjectOfType<PhoneControler>();
        interactionUI = GameObject.Find("/Canvas/InteractionUI");
        grabUI = GameObject.Find("/Canvas/GrabUI");
    }

    private void Start()
    {
        if (gameManager == null)
            throw new System.Exception("Add a gamemanager to the scene.");
        if (phone == null)
            throw new System.Exception("Add a phone controller scripts to the scene.");
        if (playerCamera == null)
            throw new System.Exception("Add a camera to the scene.");
        if (interactionUI == null)
            throw new System.Exception("Add an interaction UI to the canvas.");
        if (grabUI == null)
            throw new System.Exception("Add an grab UI to the canvas.");

        interactionUI.SetActive(true);
        grabUI.SetActive(false);

        layerMask = LayerMask.GetMask("Phone");
    }

    private void Update()
    {
        if (gameManager.IsInputEnabled)
        {
            if (Input.GetButtonDown("Phone"))
                EPressed = true;
        }
    }

    private void FixedUpdate()
    {
        if (gameManager.IsInputEnabled)
        {
            HandleRayCast();
            EPressed = false;
        }
    }

    private void HandleRayCast()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, layerMask))
        {
            if (hitInfo.transform.CompareTag("Phone"))
            {
                interactionUI.SetActive(false);
                grabUI.SetActive(true);

                if (EPressed && !phone.phoneIsOpen)
                    phone.OpenPhone();
            }
            else
            {
                interactionUI.SetActive(true);
                grabUI.SetActive(false);

                if (EPressed && phone.phoneIsOpen)
                    phone.ClosePhone();
            }
        }
    }
}
