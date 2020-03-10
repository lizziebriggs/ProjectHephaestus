using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedItemBox : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour _player;
    public void OnTriggerEnter(Collider other)
    {
        var finishedItem = other.gameObject.GetComponent<FinishedItem>();
        if (!finishedItem) return;
        if (!_player.ActiveJob) return;
        if (_player.ActiveJob.JobInformation.ItemName != finishedItem.FinalItem) return;
        //SHOULD output a percentage of the reward, needs testing
        _player.Reward += (_player.ActiveJob.JobInformation.Reward * finishedItem.Value);
        //Debug to check
        Debug.Log(_player.ActiveJob.JobInformation.Reward * finishedItem.Value);
    }
}
