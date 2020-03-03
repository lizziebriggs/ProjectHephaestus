using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCollision1 : FingerInteraction
{
    /*[SerializeField] private GameObject slab;
    public bool slabActive;*/
    
    public Material buttonColour;
    

    private void Start()
    {
        /*buttonColour.color = Color.red;*/
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finger") 
        {
            /*buttonColour.color = Color.blue;*/
            FingerInteraction.button1 = true;
            FingerInteraction.button2 = false;

        }

    }
}
