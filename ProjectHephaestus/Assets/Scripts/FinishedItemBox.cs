using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedItemBox : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour _player;
    [SerializeField] private List<ParticleSystem> _particleEffects;
    [SerializeField] private TextMesh _pointsDisplay;

    [Header("Key")]
    [SerializeField] private GameObject _nextLevelKey;
    [SerializeField] private Transform _keySpawnPoint;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        var finishedItem = other.gameObject.GetComponent<FinishedItem>();

        if (!finishedItem || !_player.ActiveJob || _player.ActiveJob.JobInformation.ItemName.ToLower() != finishedItem.FinalItem.ToLower()) return;

        switch (finishedItem.ItemQuality)
        {
            case FinishedItem.Quality.Shit:
                _particleEffects[0].Play();
                Debug.Log("Shit particles");
                break;

            case FinishedItem.Quality.Okay:
                _particleEffects[1].Play();
                Debug.Log("Okay particles");
                break;

            case FinishedItem.Quality.Good:
                _particleEffects[2].Play();
                Debug.Log("Good particles");
                break;

            case FinishedItem.Quality.Perfect:
                _particleEffects[3].Play();
                Debug.Log("Perfect particles");
                break;

            default:
                break;
        }

        _player.ActiveJob.IsCompleted = true;

        //SHOULD output a percentage of the reward, needs testing
        float playerPoints = _player.ActiveJob.JobInformation.Reward * finishedItem.RewardValue;
        _player.Reward += playerPoints;
        _pointsDisplay.text = playerPoints.ToString();

        //Debug to check
        Debug.Log(_player.ActiveJob.JobInformation.Reward * finishedItem.RewardValue);

        if (AllJobsCompleted()) SpawnKey();
    }

    private bool AllJobsCompleted()
    {
        int finishedJobCount = 0;

        foreach (var job in _player.CurrentJobs)
        {
            if (job.IsCompleted) finishedJobCount++;
        }

        if (finishedJobCount == _player.CurrentJobs.Count) return true;

        return false;
    }

    private void SpawnKey()
    {
        if(_nextLevelKey)
        {
            GameObject newKey = Instantiate(_nextLevelKey);
            newKey.GetComponent<Rigidbody>().AddForce(0, 0, 1, ForceMode.Impulse);
        }
    }
}
