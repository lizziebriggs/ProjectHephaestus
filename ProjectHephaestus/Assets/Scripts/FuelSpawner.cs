using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSpawner : MonoBehaviour
{
    [SerializeField] private Fuel[] _fuel;
    public void SpawnFuel()
    {
        var pickFuel = Random.Range(0, _fuel.Length);
        var newFuel = Instantiate(_fuel[pickFuel]);
        newFuel.transform.position = transform.position;
    }
}
