using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerInteraction : MonoBehaviour
{
    /*[SerializeField] private GameObject player;*/
    [SerializeField] private Transform target;
    public static bool button1;
    public static bool button2;
    

    private void FixedUpdate()
    {
        transform.position = target.position;
    }

    private void Update()
    {
        if (button1 == true)
        {
            Debug.Log("Button1 active");
            
        }

        else if (button2 == true)
        {
            Debug.Log("Button2 is active");
        }
    }
}





















/*if (other.gameObject.tag == "Button1")
        {
            slab1.task = true;
            slab2.task = false;
        }
        if (other.gameObject.tag == "Button2")
        {
            slab1.task = false;
            slab2.task = true;
        }*/