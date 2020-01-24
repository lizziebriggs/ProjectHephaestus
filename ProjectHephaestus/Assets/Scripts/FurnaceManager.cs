using OculusSampleFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceManager : MonoBehaviour
{
    private enum FurnaceState { Idle, Smelting, Smelted };

    private FurnaceState currentState;

    [Header("Smelting")]
    [SerializeField] Transform _smeltedObjectSpawn;
    private float _timerCountdown;
    private GameObject _furnaceObject;
    private GameObject _smeltedObject;

    [Header("Effects")]
    [SerializeField] private GameObject _fireParticleEffect;
    [SerializeField] private GameObject _smokeParticleEffect;
    [SerializeField] private float _smokeDuration;
    private float _smokeTimerCountdown;


    private void Start()
    {
        currentState = FurnaceState.Idle;
        _timerCountdown = 0;

        _fireParticleEffect.SetActive(false);
        _smokeParticleEffect.SetActive(false);
    }


    private void Update()
    {
        switch (currentState)
        {
            case FurnaceState.Idle:
                // On trigger enter switches to smelting state
                break;

            case FurnaceState.Smelting:
                Smelting();
                break;

            case FurnaceState.Smelted:
                PlaySmokePuff();
                break;

            default:
                break;
        }
    }


    private void Smelting()
    {
        _fireParticleEffect.SetActive(true);

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

        _fireParticleEffect.SetActive(false);

        _smokeTimerCountdown = _smokeDuration; ;
        currentState = FurnaceState.Smelted;
    }


    private void PlaySmokePuff()
    {
        _smokeParticleEffect.SetActive(true);

        _smokeTimerCountdown -= Time.deltaTime;
        if (_smokeTimerCountdown > 0) return;

        _smokeParticleEffect.SetActive(false);
        currentState = FurnaceState.Idle;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Smelt>())
        {
            var timeToSmelt = other.GetComponent<Smelt>().SmeltingTime;
            var smeltObject = other.GetComponent<Smelt>();

            // If the object is smeltable
            if (smeltObject && smeltObject.canBeSmelted)
            {
                _smeltedObject = smeltObject.Smelted;
                _furnaceObject = smeltObject.gameObject;

                // If the object isn't already the object to be smelted into
                if (smeltObject != _smeltedObject)
                {
                    var grabbable = other.GetComponent<DistanceGrabbable>();

                    // If the object can be grabbed then smelt
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
