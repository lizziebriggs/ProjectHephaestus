using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalleableMaterial : MonoBehaviour
{
    [SerializeField] int hitCount;
    [SerializeField] GameObject hammered;
    [SerializeField, Range(0, 1)] float[] _thresholdValues;
    [SerializeField] float _speed;

    [HideInInspector] public bool isMalleable = false;

    public float[] ThresholdValues => _thresholdValues;
    public float Speed => _speed;
    public GameObject Hammered => hammered;
    public int HitCount => hitCount;
    public int HitCounter => hitCounter;

    private int hitCounter;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<HammerController>() && isMalleable)
        {
            hitCounter++;
        }
    }
}
