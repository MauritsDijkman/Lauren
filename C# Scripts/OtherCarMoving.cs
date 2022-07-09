using UnityEngine;
using UnityEngine.SceneManagement;

public class OtherCarMoving : MonoBehaviour
{
    private GameManager gameManager;

    private GameObject playerCar;
    [SerializeField] private float movingSpeed = 5f;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerCar = GameObject.Find("Car");
    }

    private void Start()
    {
        if (gameManager == null)
            throw new System.Exception("Add a GameManager script to the scene.");
        if (playerCar == null)
            throw new System.Exception("Drag a playerCar to the script.");

        Vector3 rotationVector = playerCar.transform.rotation.eulerAngles;
        rotationVector.y -= 180;
        transform.rotation = Quaternion.Euler(rotationVector);
    }

    private void Update()
    {
        if (gameManager.IsInputEnabled)
        {
            if (playerCar.transform.rotation.eulerAngles.y == 0)
                transform.position -= new Vector3(0, 0, (1 * Time.deltaTime * movingSpeed));
            else if (playerCar.transform.rotation.eulerAngles.y == 90 || playerCar.transform.rotation.eulerAngles.y == 270)
                transform.position += new Vector3((1 * Time.deltaTime * movingSpeed), 0, 0);
            else if (playerCar.transform.rotation.eulerAngles.y == -90)
                transform.position += new Vector3((1 * Time.deltaTime * movingSpeed), 0, 0);
            else if (playerCar.transform.rotation.eulerAngles.y == -180 || playerCar.transform.rotation.eulerAngles.y == 180)
                transform.position += new Vector3(0, 0, (1 * Time.deltaTime * movingSpeed));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            SceneManager.LoadScene("Crash");
        }
    }
}
