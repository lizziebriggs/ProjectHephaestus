using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _gauges;

    private void Start()
    {
        foreach (GameObject gauge in _gauges)
            gauge.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<DistanceGrabbable>())
        {
            foreach (GameObject gauge in _gauges)
                gauge.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DistanceGrabbable>())
        {
            foreach (GameObject gauge in _gauges)
                gauge.SetActive(false);
        }
    }
}
