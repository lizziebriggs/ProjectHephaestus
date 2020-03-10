using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private ButtonInteraction _activeJob;

    public ButtonInteraction ActiveJob => _activeJob;
    public float Reward { get; set; }

    public void SetActiveJob(ButtonInteraction newJob)
    {
        if (!_activeJob)
        {
            _activeJob = newJob;
            _activeJob.ButtonColour.color = _activeJob.ActiveColour;
        }
        if (_activeJob != newJob)
        {
            //new job logic goes here
            //reset active job colour
            
            _activeJob.ButtonColour.color = _activeJob.InactiveColour;
            newJob.ButtonColour.color = newJob.ActiveColour;
            //active job = new job
            _activeJob = newJob;
        }
    }
}
