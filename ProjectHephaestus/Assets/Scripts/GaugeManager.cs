using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _anvilTimerGauge;

    private void Start()
    {
        foreach (var Gauge in _anvilTimerGauge)
            Gauge.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<DistanceGrabbable>() == true)
        {
            foreach (var Gauge in _anvilTimerGauge)
                Gauge.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DistanceGrabbable>() == true)
        {
            foreach (var Gauge in _anvilTimerGauge)
                Gauge.SetActive(false);
        }
    }
}
