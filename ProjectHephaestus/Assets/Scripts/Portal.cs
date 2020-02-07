using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject _exit;

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = _exit.transform.position;
    }
}
