using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractTester : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private int reward;
    [SerializeField, TextArea] private String description;
    [SerializeField] private Image itemImage;
    [SerializeField] private Text uiText;

    private void Awake()
    {
        uiText.text = description;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 12) Debug.Log("Hit");
    }
}
