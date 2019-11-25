using OculusSampleFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceManager : MonoBehaviour
{
    [SerializeField] int _counter;
    [SerializeField] Transform _furnaceSpawn;

    private System.Timers.Timer _timer;
    private int _elapsedCounter;

    private GameObject _furnaceObject;
    private GameObject _switchFurnaceObject;

    private void Start()
    {
        _elapsedCounter = _counter;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Smelt>() && other.GetComponent<Smelt>().canBeSmelted)
        {
            _switchFurnaceObject = other.GetComponent<Smelt>().Smelted;

            if (other.gameObject != _switchFurnaceObject)
            {
                if (other.GetComponent<DistanceGrabbable>())
                {
                    SetTimer();
                    FurnaceObjectCreation(other.gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DistanceGrabbable>())
        {
            _timer.Stop();
            _elapsedCounter = _counter;
        }
    }

    private void SetTimer()
    {
        // Create a timer with a two second interval.
        _timer = new System.Timers.Timer(1000);

        // Hook up the Elapsed event for the timer. 
        _timer.Elapsed += timer1_Tick;
        _timer.AutoReset = true;
        _timer.Enabled = true;
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        _elapsedCounter--;
        if (_elapsedCounter == 0)
        {
            _timer.Stop();
        }
    }

    private void FurnaceObjectCreation(GameObject originalObject)
    {
        // Change original item to smelted version
        Destroy(originalObject);
        Instantiate(_switchFurnaceObject);
        
        // Set position of new smelted object to original object
        _switchFurnaceObject.transform.position = originalObject.transform.position;

        // Reset timer
        _timer.Stop();
        _elapsedCounter = _counter;
    }
}
