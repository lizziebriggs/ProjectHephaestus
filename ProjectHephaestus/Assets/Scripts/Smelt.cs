using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelt : MonoBehaviour
{
    [SerializeField] private GameObject _smelted;
    [SerializeField] private float _smeltingTime;
    public float SmeltingTime => _smeltingTime;
    public GameObject Smelted => _smelted;
    public bool canBeSmelted;
}
