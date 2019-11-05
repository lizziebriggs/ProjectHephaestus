using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeManager : MonoBehaviour
{
    [SerializeField] private GameObject _anvilTimerGauge;

    private void Start()
    {
        _anvilTimerGauge.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DistanceGrabbable>() == true)
        {
            _anvilTimerGauge.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DistanceGrabbable>() == true)
        {
            _anvilTimerGauge.SetActive(false);
        }
    }
}
