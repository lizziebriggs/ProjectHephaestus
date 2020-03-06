using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : MonoBehaviour
{
    [SerializeField] private FurnaceManager furnace;

    private List<GameObject> fuel;

    private void OnTriggerEnter(Collider other)
    {
        Fuel fuelAdded = other.GetComponent<Fuel>();

        if(fuelAdded)
        {
            furnace.Fuel += fuelAdded.FuelValue;
            Debug.Log(furnace.Fuel);
        }
    }
}
