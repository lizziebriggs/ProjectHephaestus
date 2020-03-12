using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedItemBox : MonoBehaviour
{
    public PlayerBehaviour Player { get; set; }
    [SerializeField] private List<ParticleSystem> _particleEffects;
    [SerializeField] private TextMesh _pointsDisplay;
    [SerializeField] private AudioSource _finishedItemBoxAudio;

    [Header("Key")]
    [SerializeField] private GameObject _nextLevelKey;
    [SerializeField] private Transform _keySpawnPoint;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + " in final item box");

        var finishedItem = other.gameObject.GetComponent<FinishedItem>();

        //if (!finishedItem || !_player.ActiveJob || _player.ActiveJob.JobInformation.ItemName.ToLower() != finishedItem.FinalItemName.ToLower()) return;

        if(finishedItem && Player.ActiveJob.JobInformation.ItemName.ToLower() == finishedItem.FinalItemName.ToLower())
        {
            _finishedItemBoxAudio.Play();

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

            Player.ActiveJob.IsCompleted = true;

            //SHOULD output a percentage of the reward, needs testing
            float playerPoints = Player.ActiveJob.JobInformation.Reward * finishedItem.RewardValue;
            Player.Reward += playerPoints;
            _pointsDisplay.text = playerPoints.ToString();

            //Debug to check
            Debug.Log(Player.ActiveJob.JobInformation.Reward * finishedItem.RewardValue);

            if (AllJobsCompleted()) SpawnKey();

            Destroy(finishedItem.gameObject);
        }
    }

    private bool AllJobsCompleted()
    {
        int finishedJobCount = 0;

        foreach (var job in Player.CurrentJobs)
        {
            if (job.IsCompleted) finishedJobCount++;
        }

        if (finishedJobCount == Player.CurrentJobs.Count) return true;

        return false;
    }

    private void SpawnKey()
    {
        if(_nextLevelKey)
        {
            Debug.Log("Key spawned");
            GameObject newKey = Instantiate(_nextLevelKey);
            newKey.transform.position = _keySpawnPoint.position;
            newKey.GetComponent<Rigidbody>().AddForce(0, 0, 0.5f, ForceMode.Impulse);
        }
    }
}
