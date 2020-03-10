using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class MalleableMaterial : MonoBehaviour
{
    [Header("Hammering")]
    [SerializeField] int hitCount;
    [SerializeField] GameObject hammered;
    [SerializeField, Range(0, 1)] float[] _thresholdValues;
    [SerializeField, Range(0.1f, 1)] float _speed;
    [SerializeField] private GameObject[] hammeredObjects;

    [HideInInspector] public StrikerTimerController strikerTimerController;
    [HideInInspector] public AnvilManager AnvilManager;

    private const int DEVIATION_SIZE = 3;
    [Header("Smelting")]
    [SerializeField] private int[] _tempDeviations = new int[DEVIATION_SIZE];
    [HideInInspector] public bool isMalleable = false;
    [SerializeField] private float _smeltingTime;
    [SerializeField] private int _goalTemperature;
    public float SmeltingTime => _smeltingTime;
    public int GoalTemperature => _goalTemperature;

    public int[] TempDeviations => _tempDeviations;

    private float _maxPoints;
    public float finalPoints { get; set; }
    private int hitCounter;

    public float[] ThresholdValues => _thresholdValues;
    public float Speed => _speed;
    public GameObject Hammered => hammered;
    public int HitCount => hitCount;
    public int HitCounter => hitCounter;
    public float MaxPoints => _maxPoints;

    void OnValidate()
    {
        if (_tempDeviations.Length != DEVIATION_SIZE)
        {
            Debug.LogWarning("Don't fucking change the 'Temp Deviation' array size you bitch!");
            Array.Resize(ref _tempDeviations, DEVIATION_SIZE);
        }
    }


    private void Awake()
    {
        _maxPoints = HitCount;
        Debug.Log("Max points: " + _maxPoints);
    }


    public void UpdateValue()
    { 
        // Shit
        if (strikerTimerController.Fill.color == strikerTimerController.Red)
        {
            //endValue += 1f / thresholdLength 
            finalPoints += 1f / _thresholdValues.Length;
            hitCounter++;
            AnvilManager.Particles[0].Play();
            Debug.Log("Shit hit");
        }
        // Ok
        else if (strikerTimerController.Fill.color == strikerTimerController.Amber)
        {
            //endValue +=  (1 / thresholdLength) * 2
            finalPoints += (1f / _thresholdValues.Length) * 2;
            hitCounter++;
            AnvilManager.Particles[1].Play();
            Debug.Log("Ok hit");
        }
        // Good
        else if (strikerTimerController.Fill.color == strikerTimerController.Green)
        {
            finalPoints += (1f / _thresholdValues.Length) * 3;
            hitCounter++;
            AnvilManager.Particles[2].Play();
            Debug.Log("Good hit");
        }
        // Perfect
        else if (strikerTimerController.Fill.color == strikerTimerController.Blue)
        {
            finalPoints += 1f;
            hitCounter++;
            AnvilManager.Particles[3].Play();
            Debug.Log("Perfect hit");
        }

        if (hitCounter == hitCount) FinalHit();
    }

    public GameObject FinalHit()
    {
        finalPoints = Mathf.Floor(finalPoints * 100) / 100;

        Debug.Log("Final points: " + finalPoints);

        if (finalPoints <= _maxPoints / _thresholdValues.Length)
        {
            Debug.Log("Shit Item");
            return hammeredObjects[0];
        }
        // Ok
        else if (finalPoints <= (_maxPoints / _thresholdValues.Length) * 2)
        {
            Debug.Log("Ok Item");
            return hammeredObjects[1];
        }
        // Good
        else if (finalPoints < _maxPoints)
        {
            Debug.Log("Good Item");
            return hammeredObjects[2];
        }
        // Perfect
        else
        {
            Debug.Log("Perfect Item");
            return hammeredObjects[3];
        }

        //var value = _maxPoints % _finalPoints;
        //Debug.Log("Return value: " + value);
        //return hammeredObjects[(int)Mathf.Ceil(value)];
    }
}
