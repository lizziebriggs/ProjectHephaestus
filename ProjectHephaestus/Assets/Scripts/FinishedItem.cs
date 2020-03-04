using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedItem : MonoBehaviour
{
    [SerializeField] private string _finalItem;
    [SerializeField] private int _value;
    public string FinalItem => _finalItem;

    public int Value => _value;
}
