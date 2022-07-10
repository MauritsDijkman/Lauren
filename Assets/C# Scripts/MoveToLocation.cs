using UnityEngine;
using System;

public class MoveToLocation : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody rb;

    [SerializeField] private GameObject[] targetPosition;

    [NonSerialized] public float movingSpeed = 0f;
    [SerializeField] private float rotatingSpeed = 10f;
    [SerializeField] private float WPradius = 1;
    [SerializeField] private float maxMotorTorque = 400f;

    private int currentTarget = 0;
    private bool targetPositionsLeft = false;
    private float step;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (gameManager == null)
            throw new System.Exception("Add a gamemanager to the scene.");
        if (rb == null)
            throw new System.Exception("Add a rigibody to the car.");

        if (targetPosition.Length > 0)
            targetPositionsLeft = true;
    }

    private void FixedUpdate()
    {
        if (gameManager.IsInputEnabled)
        {
            MoveToTarget();
            LookAtTarget();
            ControlSpeed();
        }
    }

    private void MoveToTarget()
    {
        if (targetPositionsLeft)
        {
            if (Vector3.Distance(targetPosition[currentTarget].transform.position, transform.position) < WPradius)
            {
                if (currentTarget >= targetPosition.Length - 1)
                    targetPositionsLeft = false;
                else
                    currentTarget++;
            }

            Vector3 newPos = Vector3.MoveTowards(transform.position, targetPosition[currentTarget].transform.position, Time.fixedDeltaTime * movingSpeed);
            rb.MovePosition(newPos);
        }

        if (currentTarget == 83)
        {
            currentTarget = 6;
        }
    }

    private void LookAtTarget()
    {
        step = rotatingSpeed * Time.deltaTime;

        Quaternion newRot = Quaternion.RotateTowards(transform.rotation, targetPosition[currentTarget].transform.rotation, step);
        rb.MoveRotation(newRot);
    }

    private void ControlSpeed()
    {
        movingSpeed = maxMotorTorque * Input.GetAxis("Accelerate");
    }
}
