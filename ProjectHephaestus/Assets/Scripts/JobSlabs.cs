using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobSlabs : MonoBehaviour
{
    [SerializeField] private string _itemName;
    [SerializeField] private float _reward;
    [SerializeField, TextArea] private String _toDo;
    [SerializeField] private Text _uiText;

    public string ItemName => _itemName;
    public float Reward => _reward;

    private void Awake()
    {
        //_uiText.text = _toDo;
    }
}
