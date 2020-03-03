using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedItemBox : MonoBehaviour
{
        private FinishedItem _finishedItem;
        private ButtonInteraction _activeJob;
        [SerializeField] private PlayerBehaviour _player;

        public void OnTriggerEnter(Collider other)
        {
            
                var finalItem = other.gameObject.GetComponent<FinishedItem>();
                if (_activeJob.JobInformation.itemName == _finishedItem.finalItem)
                {
                       /*Destroy(_activeJob.gameObject);*/
                       Debug.Log("Hello");
                }
        }
        /*private void OnTriggerEnter(Collider other)
        {
            var finalItem = other.gameObject.GetComponent<FinishedItem>();
            if (finalItem) _player.SetActiveJob(buttonInteraction);
        }*/
}
