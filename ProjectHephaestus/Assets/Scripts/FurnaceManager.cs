using OculusSampleFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceManager : MonoBehaviour
{
    private enum FurnaceState { Waiting, Smelting };

    private FurnaceState currentState;

    [Header("Smelting")]
    [SerializeField] Transform _smeltedObjectSpawn;
    private float _timerCountdown;
    private GameObject _furnaceObject;
    private GameObject _smeltedObject;

    

    private void Start()
    {
        currentState = FurnaceState.Waiting;
        _timerCountdown = 0;
    }


    private void Update()
    {
        switch (currentState)
        {
            case FurnaceState.Waiting:
                // On trigger enter switches to smelting state
                break;

            case FurnaceState.Smelting:
                Smelting();
                break;

            default:
                break;
        }
    }


    private void Smelting()
    {
        _timerCountdown -= Time.deltaTime;
        if (_timerCountdown > 0) return;
        Smelt();
    }


    private void Smelt()
    {
        // Change original item to smelted version
        Destroy(_furnaceObject);
        Instantiate(_smeltedObject);

        // Set position of new smelted object to spawn position
        _smeltedObject.transform.position = _smeltedObjectSpawn.transform.position;

        currentState = FurnaceState.Waiting;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Smelt>())
        {
            var timeToSmelt = other.GetComponent<Smelt>().SmeltingTime;
            var smeltObject = other.GetComponent<Smelt>();

            if (smeltObject && smeltObject.canBeSmelted)
            {
                _smeltedObject = smeltObject.Smelted;
                _furnaceObject = smeltObject.gameObject;

                if (smeltObject != _smeltedObject)
                {
                    var grabbable = other.GetComponent<DistanceGrabbable>();
                    if (grabbable)
                    {
                        _timerCountdown = timeToSmelt;
                        currentState = FurnaceState.Smelting;
                    }
                }
            }
        }
    }
}
