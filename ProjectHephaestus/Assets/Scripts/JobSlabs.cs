using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobSlabs : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private int reward;
    [SerializeField, TextArea] private String toDo;
    [SerializeField] private Text uiText;

    public string ItemName => itemName;

    public int Reward => reward;

    private void Awake()
    {
        uiText.text = toDo;
    }
}
