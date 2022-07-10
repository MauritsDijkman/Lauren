using UnityEngine;
using System;

public class CarTrafficLightHandler : MonoBehaviour
{
    private TrafficLight trafficLight;
    private Honking honking;

    [NonSerialized] public bool carIsInFrontOfOrangeOrRedTrafficLight = false;

    private void Awake()
    {
        trafficLight = this.gameObject.GetComponentInParent<TrafficLight>();
        honking = FindObjectOfType<Honking>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            if (trafficLight.orangeLightIsActive || trafficLight.redLightIsActive)
            {
                carIsInFrontOfOrangeOrRedTrafficLight = true;
                honking.carInFrontYORTrafficLight = carIsInFrontOfOrangeOrRedTrafficLight;
            }
            if (trafficLight.greenLightIsActive)
            {
                carIsInFrontOfOrangeOrRedTrafficLight = false;
                honking.carInFrontYORTrafficLight = carIsInFrontOfOrangeOrRedTrafficLight;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            carIsInFrontOfOrangeOrRedTrafficLight = false;
            honking.carInFrontYORTrafficLight = carIsInFrontOfOrangeOrRedTrafficLight;
        }
    }
}
