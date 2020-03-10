using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedItem : MonoBehaviour
{
    [SerializeField] private string _finalItem;
    [SerializeField, Range(0, 1)] private float _value;
    public string FinalItem => _finalItem;

    public float Value => _value;
}
