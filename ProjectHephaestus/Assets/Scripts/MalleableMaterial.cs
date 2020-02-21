using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalleableMaterial : MonoBehaviour
{
    [SerializeField] int hitCount;
    [SerializeField] GameObject hammered;
    [SerializeField, Range(0, 1)] float[] _thresholdValues;
    [SerializeField] float _speed;
    [HideInInspector] public StrikerTimerController strikerTimerController;

    [HideInInspector] public bool isMalleable = false;

    private float _maxPoints;
    private float _finalPoints;
    private int hitCounter;

    private bool justHit = false;

    public float[] ThresholdValues => _thresholdValues;
    public float Speed => _speed;
    public GameObject Hammered => hammered;
    public int HitCount => hitCount;
    public int HitCounter => hitCounter;


    private void Start()
    {
        _maxPoints = HitCounter;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (!justHit && collision.gameObject.GetComponent<HammerController>() && isMalleable)
        {
            //Debug.Log("Collided with hammer");
            //Debug.Log("Hit counter: " + hitCounter);
            UpdateValue();
        }

        justHit = true;
    }


    private void OnCollisionExit(Collision collision)
    {
        justHit = false;
    }


    private void UpdateValue()
    {
        //Debug.Log("Hit");
        ////maxValue == hitCount

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
        if (_finalPoints < _maxPoints / _thresholdValues.Length)
        {
            //spawn shit object
            Debug.Log("Shit Item");
        }
        // Ok
        else if (_finalPoints < (_maxPoints / _thresholdValues.Length) * 2)
        {
            //spawn ok object
            Debug.Log("Ok Item");
        }
        // Good
        else if (_finalPoints < (_maxPoints / _thresholdValues.Length) * 3)
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
