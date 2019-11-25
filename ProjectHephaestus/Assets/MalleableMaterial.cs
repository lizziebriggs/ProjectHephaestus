using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalleableMaterial : MonoBehaviour
{
    [SerializeField] int hitCount;
    [SerializeField] GameObject hammered;

    //public bool isMalleable;

    private int hitCounter;

    void Start()
    {
        
    }


    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<HammerController>())
        {
            Debug.Log(hitCounter);
            hitCounter++;

            if (hitCounter == hitCount)
            {
                GameObject hammeredObject = Instantiate(hammered);
                hammeredObject.transform.position = transform.position;

                Destroy(gameObject);
            }
        }
    }
}
