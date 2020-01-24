using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalleableMaterial : MonoBehaviour
{
    [SerializeField] int hitCount;
    [SerializeField] GameObject hammered;

    private int hitCounter;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<HammerController>())
        {
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
