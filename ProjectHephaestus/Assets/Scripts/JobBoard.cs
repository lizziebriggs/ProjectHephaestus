using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobBoard : MonoBehaviour
{
    [SerializeField] public GameObject leftJob;
    [SerializeField] public GameObject rightJob;

    [SerializeField] public GameObject leftJobCol;
    [SerializeField] public GameObject rightJobCol;

    public bool Active;
    public bool PreviousActive;
    public bool NotActive;

    void spawnLeft()
    {
        Instantiate(leftJob, transform.position, Quaternion.identity);
    }

    void spawnRight()
    {
        Instantiate(rightJob, transform.position, Quaternion.identity);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "leftJobCol")
        {
            NotActive = false;
            Active = true; 
        }

        if(collision.gameObject.tag == "TaskCol")
        {
            NotActive = true;
            Active = false;
        }

    }
}
