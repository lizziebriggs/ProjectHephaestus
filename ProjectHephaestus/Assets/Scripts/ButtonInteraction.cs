using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    //Attach your job here for each slab
    [SerializeField] private JobSlabs _jobInformation;
    //this makes the job information accessible in a way that other scripts can't change the information
    public JobSlabs JobInformation => _jobInformation;
    public Material ButtonColour => GetComponent<MeshRenderer>().material;
    
    public Color ActiveColour => Color.blue;
    public Color InactiveColour => Color.red;
}
