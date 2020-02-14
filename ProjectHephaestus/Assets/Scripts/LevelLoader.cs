using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private enum LevelState { Idle, LevelLoaded }
    [SerializeField] private Transform _objectPoint;
    [SerializeField] private GameObject _portal;
    private LevelItem _currentLevel;
    private LevelState _currentState = LevelState.Idle;
    private int _speed = 50;

    private void Start()
    {
        if (_portal.activeSelf) _portal.SetActive(false);
    }

    private void Update()
    {
        switch (_currentState)
        {
            default:
                break;
            case LevelState.Idle:
                break;
            case LevelState.LevelLoaded:
                LevelLoadedState();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var level = other.GetComponent<LevelItem>();
        if (level)
        {
            if (!_currentLevel)
            {
                _currentLevel = level;
                _currentState = LevelState.LevelLoaded;
                SceneManager.LoadScene(level.Level, LoadSceneMode.Additive);
                if (_portal.activeSelf != true) _portal.SetActive(true);
            }

            else if (_currentLevel != level)
            {
                SceneManager.UnloadSceneAsync(_currentLevel.Level);
                SceneManager.LoadScene(level.Level, LoadSceneMode.Additive);
                _currentLevel = level;
                _currentState = LevelState.LevelLoaded;
                if (_portal.activeSelf != true) _portal.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var level = other.GetComponent<LevelItem>();
        if (level)
        {


            if (_currentLevel)
            {
                SceneManager.UnloadSceneAsync(_currentLevel.Level);
                if (_portal.activeSelf != false) _portal.SetActive(false);
                _currentState = LevelState.Idle;
                _currentLevel = null;
            }
        }
    }

    private void LevelLoadedState()
    {
        if (_currentLevel) // if anvil object isn't null
        {
            var levelRigidbody = _currentLevel.GetComponent<Rigidbody>(); // get the rigidbody
            if (!levelRigidbody.isKinematic) // when the object is not being held
            {
                _currentLevel.gameObject.transform.up = transform.up; // set up rotation
                _currentLevel.gameObject.transform.position = transform.position;// hold position on the anvil

            }
            _currentLevel.gameObject.transform.Rotate(0, 0, _speed * Time.deltaTime);
        }
    }
}
