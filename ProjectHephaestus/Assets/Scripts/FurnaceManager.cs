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
    [SerializeField] GameObject _smeltingTimerDisplay;
    [SerializeField] TextMesh _smeltingTimerMesh;
    private float _smeltingCountdown;
    private GameObject _furnaceObject;
    private GameObject _smeltedObject;

    [Header("Fuel")]
    public int fuel;
    [SerializeField] private float _burnSpeed;
    public float BurnSpeed => _burnSpeed;
    private float _fuelTimer;


    [Header("Effects")]
    [SerializeField] private GameObject _fireParticleEffect;
    [SerializeField] private GameObject _smokeParticleEffect;
    [SerializeField] private float _smokeDuration;
    private float _smokeTimerCountdown;

    [Header("Striker Timer Controller")]
    [SerializeField] private StrikerTimerController _strikerTimerController;


    private void Start()
    {
        currentState = FurnaceState.Idle;
        _smeltingCountdown = 0;

        fuel = 0;
        _fuelTimer = _burnSpeed;

        _smeltingTimerDisplay.SetActive(false);
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
                if (fuel > 0)
                    Smelting();
                else _smeltingTimerMesh.text = "Out of Fuel!";
                break;

            case FurnaceState.Smelted:
                PlaySmokePuff();
                break;

            default:
                break;
        }

        if (fuel > 0)
        {
            _fireParticleEffect.SetActive(true);
            BurnFuel();
        }
        else _fireParticleEffect.SetActive(false);
    }

    private void BurnFuel()
    {
        _fuelTimer -= Time.deltaTime;

        if (_fuelTimer <= 0)
        {
            fuel--;
            _fuelTimer = _burnSpeed;
            return;
        }
    }


    private void Smelting()
    {
        // Round up to whole number
        _smeltingTimerMesh.text = Math.Ceiling(_smeltingCountdown).ToString();

        _smeltingCountdown -= Time.deltaTime;
        if (_smeltingCountdown > 0) return;

        // When timer has finished
        Smelt();
    }


    private void Smelt()
    {
        // Change original item to smelted version
        Destroy(_furnaceObject);
        var smelted = Instantiate(_smeltedObject);

        // Set position of new smelted object to spawn position
        smelted.transform.position = _smeltedObjectSpawn.transform.position;
        var _smeltedComponent = smelted.GetComponent<MalleableMaterial>();
        _smeltedComponent.strikerTimerController = _strikerTimerController;

        _smeltingTimerDisplay.SetActive(false);
        //_fireParticleEffect.SetActive(false);

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
        // Check for smeltable item
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
                        _smeltingCountdown = timeToSmelt;

                        _smeltingTimerDisplay.SetActive(true);
                        //_fireParticleEffect.SetActive(true);

                        currentState = FurnaceState.Smelting;
                    }
                }
            }
        }
    }
}
