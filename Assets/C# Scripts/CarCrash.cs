using UnityEngine;

public class CarCrash : MonoBehaviour
{
    private GameManager gameManager;
    private RayCast raycast;
    private DialogueSystem dialogue;
    private PhoneControler phone;
    private Instagram instagram;

    private bool otherCarSpawned = false;
    private Vector3 spawnPosition;

    [SerializeField] private GameObject otherCar;
    [SerializeField] private AudioSource alert;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        raycast = FindObjectOfType<RayCast>();
        dialogue = FindObjectOfType<DialogueSystem>();
        phone = FindObjectOfType<PhoneControler>();
        instagram = FindObjectOfType<Instagram>();
    }

    private void Start()
    {
        if (gameManager == null)
            throw new System.Exception("Add a GameManager script to the scene.");
        if (raycast == null)
            throw new System.Exception("Add a RayCast script to the scene");
        if (dialogue == null)
            throw new System.Exception("Add a DialogueSystem script to the scene");
        if (phone == null)
            throw new System.Exception("Add a PhoneControler script to the scene");
        if (instagram == null)
            throw new System.Exception("Add a Instagram script to the scene");
    }

    private void Update()
    {
        if (gameManager.IsInputEnabled)
            HandleCarCrash();
    }

    private void HandleCarCrash()
    {
        if (instagram.playerCheckedMeme && phone.phoneIsOpen)
        {
            if (!otherCarSpawned)
            {
                dialogue.StopDialogueAudio();
                alert.Play();

                if (transform.rotation.eulerAngles.y == 0)
                    spawnPosition = new Vector3(transform.position.x, 0, transform.position.z + 120);
                else if (transform.rotation.eulerAngles.y == 90)
                    spawnPosition = new Vector3(transform.position.x + 120, 0, transform.position.z);
                else if (transform.rotation.eulerAngles.y == -90 || transform.rotation.eulerAngles.y == 270)
                    spawnPosition = new Vector3(transform.position.x - 120, 0, transform.position.z);
                else if (transform.rotation.eulerAngles.y == -180 || transform.rotation.eulerAngles.y == 180)
                    spawnPosition = new Vector3(transform.position.x, 0, transform.position.z - 120);

                Instantiate(otherCar, spawnPosition, Quaternion.identity);
                otherCarSpawned = true;
            }
        }
    }
}
