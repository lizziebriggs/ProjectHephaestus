using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialColour : MonoBehaviour
{ 
    [SerializeField] Color colour;

    void Start()
    {
        GetComponent<Renderer>().material.color = colour;
    }
}
