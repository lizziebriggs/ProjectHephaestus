using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalleableMaterial : MonoBehaviour
{
    [SerializeField] int hitCount;
    [SerializeField] GameObject hammered;
    [SerializeField, Range(0, 1)] float[] _thresholdValues;
    [SerializeField, Range(0.1f, 1)] float _speed;
    [HideInInspector] public StrikerTimerController strikerTimerController;

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
            Debug.Log("Shit hit");
        }
        // Ok
        else if (strikerTimerController.Fill.color == strikerTimerController.Amber)
        {
            //endValue +=  (1 / thresholdLength) * 2
            _finalPoints += (1f / _thresholdValues.Length) * 2;
            hitCounter++;
            Debug.Log("Ok hit");
        }
        // Good
        else if (strikerTimerController.Fill.color == strikerTimerController.Green)
        {
            _finalPoints += (1f / _thresholdValues.Length) * 3;
            hitCounter++;
            Debug.Log("Good hit");
        }
        // Perfect
        else if (strikerTimerController.Fill.color == strikerTimerController.Blue)
        {
            _finalPoints += 1f;
            hitCounter++;
            Debug.Log("Perfect hit");
        }

        if (hitCounter == hitCount) FinalHit();
    }

    private void FinalHit()
    {
        _finalPoints = Mathf.Floor(_finalPoints * 100) / 100;

        Debug.Log("Final points: " + _finalPoints);
        

        if (_finalPoints <= _maxPoints / _thresholdValues.Length)
        {
            Debug.Log(_maxPoints / _thresholdValues.Length);
            //spawn shit object
            Debug.Log("Shit Item");
        }
        // Ok
        else if (_finalPoints <= (_maxPoints / _thresholdValues.Length) * 2)
        {
            Debug.Log((_maxPoints / _thresholdValues.Length) * 2);
            //spawn ok object
            Debug.Log("Ok Item");
        }
        // Good
        else if (_finalPoints < _maxPoints)
        {
            //good item
            Debug.Log("Good Item");
        }
        // Perfect
        else if (_finalPoints >= _maxPoints)
        {
           //perfect item
            Debug.Log("Perfect Item");
        }

    }

    
    /*
     * method ValueUpdate(value)
     * 
     *bunch of if statements
     * if value == _threshold1
     * totalValueCounter+=0.1
     * hitCounter++
     * 
     * if hitCounter == max
     * bunch of ifs statement
     * if totalValueCounter == shitRange
     * instantiate(shitItem)
     */
}
