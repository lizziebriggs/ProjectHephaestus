using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : MonoBehaviour
{
    [SerializeField] private FurnaceManager furnace;
    [SerializeField] private FuelSpawner _fuelSpawner;

    private void OnTriggerEnter(Collider other)
    {
        Fuel fuelAdded = other.GetComponent<Fuel>();

        if (!fuelAdded) return;
        furnace.AddFuel(fuelAdded);
        fuelAdded.Furnace = furnace;
        _fuelSpawner.SpawnFuel();
    }
}
