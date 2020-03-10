using OculusSampleFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceManager : MonoBehaviour
{
    private enum FurnaceState { Idle, Smelting, Smelted, AwaitingItem };

    private FurnaceState currentState;

    [Header("Smelting")]
    [SerializeField] Transform _smeltedObjectSpawn;
    [SerializeField] GameObject _smeltingTimerDisplay;
    [SerializeField] TextMesh _smeltingTimerMesh;
    [SerializeField] FurnaceButtons[] _smeltingObjects;
    private float _smeltingCountdown;
    private GameObject _furnaceObject;
    public GameObject _smeltedObject { get; set; }

    [Header("Fuel")]
    public int Fuel;
    [SerializeField] private float _burnSpeed;
    [SerializeField] private float _tempSpeed;
    [SerializeField] private Text _tempDisplay;
    public float BurnSpeed => _burnSpeed;
    private float _fuelTimer;


    [Header("Effects")]
    [SerializeField] private GameObject _fireParticleEffect;
    [SerializeField] private GameObject _smokeParticleEffect;
    [SerializeField] private float _smokeDuration;
    private float _smokeTimerCountdown;

    [Header("Striker Timer Controller")]
    [SerializeField] private StrikerTimerController _strikerTimerController;

    [HideInInspector] public List<Fuel> _fuelCount = new List<Fuel>();
    private float _temp;
    public float TempValue { get; set; }


    private void Start()
    {
        currentState = FurnaceState.Idle;
        _smeltingCountdown = 0;

        Fuel = 0;
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
                if (Fuel > 0)
                    Smelting();
                else _smeltingTimerMesh.text = "Out of Fuel!";
                break;

            case FurnaceState.Smelted:
                PlaySmokePuff();
                break;

            case FurnaceState.AwaitingItem:
                AwaitSelection();
                break;

            default:
                break;
        }

        if (Fuel > 0)
        {
            _fireParticleEffect.SetActive(true);
            BurnFuel();
        }
        else
        {
            _fireParticleEffect.SetActive(false);
            if (_temp > 0) _temp -= Time.deltaTime;
        }

        var tempInt = (int)Math.Round(_temp);
        _tempDisplay.text = tempInt + "°C";
    }

    private void BurnFuel()
    {
        _fuelTimer -= Time.deltaTime;
        
        if (_temp < TempValue) _temp += Time.deltaTime * _tempSpeed;
        else if (_temp > TempValue) _temp -= Time.deltaTime * _tempSpeed;

        if (_fuelCount.Count != 0 && !_fuelCount[0]._canBurn)
        {
            _fuelCount[0].StartBurning(_burnSpeed);
        }

        if (_fuelTimer <= 0)
        {
            Fuel--;
            _fuelTimer = _burnSpeed;
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
        _smeltedObject = null;

        var smeltedComponent = smelted.GetComponent<MalleableMaterial>();
        smeltedComponent.finalPoints -= CalculateDeduction(_temp, smeltedComponent);

        // Set position of new smelted object to spawn position
        smelted.transform.position = _smeltedObjectSpawn.transform.position;
        var _smeltedComponent = smelted.GetComponent<MalleableMaterial>();
        _smeltedComponent.strikerTimerController = _strikerTimerController;
        Debug.Log(smeltedComponent.finalPoints);
        _smeltingTimerDisplay.SetActive(false);

        _smokeTimerCountdown = _smokeDuration; ;
        currentState = FurnaceState.Smelted;
    }

    private float CalculateDeduction(float _currentTemp, MalleableMaterial smeltedObject)
    {
        // Perfect
        if (_currentTemp > (smeltedObject.GoalTemperature - smeltedObject.TempDeviations[0]) && _currentTemp < (smeltedObject.GoalTemperature + smeltedObject.TempDeviations[0]))
            return 0;

        // Good
        else if (_currentTemp > (smeltedObject.GoalTemperature - smeltedObject.TempDeviations[1]) && _currentTemp < (smeltedObject.GoalTemperature + smeltedObject.TempDeviations[1]))
            return smeltedObject.MaxPoints / smeltedObject.ThresholdValues.Length;

        // Okay
        else if (_currentTemp > (smeltedObject.GoalTemperature - smeltedObject.TempDeviations[2]) && _currentTemp < (smeltedObject.GoalTemperature + smeltedObject.TempDeviations[2]))
            return (smeltedObject.MaxPoints / smeltedObject.ThresholdValues.Length) * 2;

        // Shit
        else return (smeltedObject.MaxPoints / smeltedObject.ThresholdValues.Length) * 3;
    }

    private void PlaySmokePuff()
    {
        _smokeParticleEffect.SetActive(true);

        _smokeTimerCountdown -= Time.deltaTime;
        if (_smokeTimerCountdown > 0) return;

        _smokeParticleEffect.SetActive(false);
        currentState = FurnaceState.Idle;
    }

    public void AddFuel(Fuel fuel)
    {
        if (!_fuelCount.Contains(fuel))
        {
            Fuel += fuel.FuelValue;
            TempValue += fuel.TempValue;
            _fuelCount.Add(fuel);
        }
    }

    private void AwaitSelection()
    {
        if (_smeltedObject)
        {
            foreach(var item in _smeltingObjects)
            {
                if (item.gameObject.activeSelf) item.gameObject.SetActive(false);
            }
            var timeToSmelt = _smeltedObject.GetComponent<MalleableMaterial>().SmeltingTime;

            _smeltingCountdown = timeToSmelt;

            _smeltingTimerDisplay.SetActive(true);

            currentState = FurnaceState.Smelting;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // Check for smeltable item
        if (other.GetComponent<Smelt>())
        {
        
            var smeltObject = other.GetComponent<Smelt>();

            // If the object is smeltable
            if (smeltObject && smeltObject.canBeSmelted)
            {
                _furnaceObject = smeltObject.gameObject;

                // If the object isn't already the object to be smelted into
                if (smeltObject != _smeltedObject)
                {
                    var grabbable = other.GetComponent<DistanceGrabbable>();

                    // If the object can be grabbed then smelt
                    if (grabbable)
                    {
                        foreach(var item in _smeltingObjects)
                        {
                            if (!item.gameObject.activeSelf) item.gameObject.SetActive(true);
                        }
                        currentState = FurnaceState.AwaitingItem;
                    }
                }
            }
        }
    }
}
