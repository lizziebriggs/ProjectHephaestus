using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelt : MonoBehaviour
{
    [SerializeField] private GameObject _smelted;
    public GameObject Smelted => _smelted;
    public bool canBeSmelted;
}
