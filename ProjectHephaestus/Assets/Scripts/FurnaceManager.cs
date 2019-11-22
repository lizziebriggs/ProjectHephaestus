using OculusSampleFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceManager : MonoBehaviour
{
    private System.Timers.Timer _timer;
    [SerializeField] int _counter;
    [SerializeField] Transform _furnaceSpawn;
    private int _elapsedCounter;
    private GameObject _furnaceObject;
    private GameObject _switchFurnaceObject;
    bool _spawn = true;

    private void Start()
    {
        _elapsedCounter = _counter;
    }

    private void Update()
    {
        if (_spawn) return;
        FurnaceObjectCreation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _furnaceSpawn) _spawn = true;
        else _spawn = false;
        Debug.Log(_spawn);
        if (other.GetComponent<DistanceGrabbable>() == true)
        {
            GameObject furnaceObject = other.GetComponent<Smelt>().Smelted;
            if (furnaceObject)
            {
                _furnaceObject = other.gameObject;
                _switchFurnaceObject = furnaceObject;
            }

            SetTimer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DistanceGrabbable>() == true)
        {
            _timer.Stop();
            _elapsedCounter = _counter;
            _spawn = true;
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

    private void FurnaceObjectCreation()
    {
        _furnaceObject.gameObject.SetActive(false);
        Instantiate(_switchFurnaceObject);
        _switchFurnaceObject.transform.position = _furnaceSpawn.position;
        _timer.Stop();
        _elapsedCounter = _counter;
        _spawn = true;
    }
}
