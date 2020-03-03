using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobSlabs : MonoBehaviour
{
    [SerializeField] public string itemName;
    [SerializeField] public int reward;
    [SerializeField, TextArea] private String toDo;
    [SerializeField] private Text uiText;
    // Start is called before the first frame update
    void Start()
    {
        uiText.text = toDo;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
