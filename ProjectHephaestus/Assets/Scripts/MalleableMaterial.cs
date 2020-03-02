using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class MalleableMaterial : MonoBehaviour
{
    [SerializeField] int hitCount;
    [SerializeField] GameObject hammered;
    [SerializeField, Range(0, 1)] float[] _thresholdValues;
    [SerializeField, Range(0.1f, 1)] float _speed;
    [HideInInspector] public StrikerTimerController strikerTimerController;
    [HideInInspector] public AnvilManager AnvilManager;

    [SerializeField] private GameObject[] hammeredObjects;
    

    [HideInInspector] public bool isMalleable = false;

    private float _maxPoints;
    private float _finalPoints;
    private int hitCounter;

    public float[] ThresholdValues => _thresholdValues;
    public float Speed => _speed;
    public GameObject Hammered => hammered;
    public int HitCount => hitCount;
    public int HitCounter => hitCounter;


    private void Start()
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
            _finalPoints += 1f / _thresholdValues.Length;
            hitCounter++;
            AnvilManager.Particles[0].Play();
            Debug.Log("Shit hit");
        }
        // Ok
        else if (strikerTimerController.Fill.color == strikerTimerController.Amber)
        {
            //endValue +=  (1 / thresholdLength) * 2
            _finalPoints += (1f / _thresholdValues.Length) * 2;
            hitCounter++;
            AnvilManager.Particles[1].Play();
            Debug.Log("Ok hit");
        }
        // Good
        else if (strikerTimerController.Fill.color == strikerTimerController.Green)
        {
            _finalPoints += (1f / _thresholdValues.Length) * 3;
            hitCounter++;
            AnvilManager.Particles[2].Play();
            Debug.Log("Good hit");
        }
        // Perfect
        else if (strikerTimerController.Fill.color == strikerTimerController.Blue)
        {
            _finalPoints += 1f;
            hitCounter++;
            AnvilManager.Particles[3].Play();
            Debug.Log("Perfect hit");
        }

        if (hitCounter == hitCount) FinalHit();
    }

    public GameObject FinalHit()
    {
        _finalPoints = Mathf.Floor(_finalPoints * 100) / 100;

        Debug.Log("Final points: " + _finalPoints);

        if (_finalPoints <= _maxPoints / _thresholdValues.Length)
        {
            Debug.Log("Shit Item");
            return hammeredObjects[0];
        }
        // Ok
        else if (_finalPoints <= (_maxPoints / _thresholdValues.Length) * 2)
        {
            Debug.Log("Ok Item");
            return hammeredObjects[1];
        }
        // Good
        else if (_finalPoints < _maxPoints)
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
