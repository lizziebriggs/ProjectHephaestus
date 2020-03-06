using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : MonoBehaviour
{
    [SerializeField] private FurnaceManager furnace;

    private void OnTriggerEnter(Collider other)
    {
        Fuel fuelAdded = other.GetComponent<Fuel>();

        if (fuelAdded)
        {
            furnace.AddFuel(fuelAdded);
            fuelAdded._furnace = furnace;

            fuelAdded.StartBurning(furnace.BurnSpeed);
        }
    }
}
