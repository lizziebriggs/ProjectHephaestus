using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedItem : MonoBehaviour
{
    public enum Quality { Shit, Okay, Good, Perfect };

    [SerializeField] private string _finalItem;
    [SerializeField, Range(0, 1)] private float _rewardValue;
    [SerializeField] private Quality _itemQuality;
    public string FinalItem => _finalItem;

    public float RewardValue => _rewardValue;

    public Quality ItemQuality => _itemQuality;
}
