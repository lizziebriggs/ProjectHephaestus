using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private enum LevelState { Idle, LevelLoaded }
    [SerializeField] private PlayerBehaviour _player;
    [SerializeField] private Transform _objectPoint;
    [SerializeField] private GameObject _portal;
    [SerializeField] private ParticleSystem _particleSystem;
    private AsyncOperation asyncLoad;
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
            StartCoroutine(LoadScene(level));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var level = other.GetComponent<LevelItem>();
        if (level)
        {
            StartCoroutine(UnloadScene(level));
        }
    }

    private IEnumerator LoadScene(LevelItem level)
    {
        if (!_currentLevel)
        {
            _currentLevel = level;
            _currentState = LevelState.LevelLoaded;
            asyncLoad = SceneManager.LoadSceneAsync(level.Level, LoadSceneMode.Additive);
            if (!_particleSystem.gameObject.activeSelf) _particleSystem.gameObject.SetActive(true);
            if (_portal.activeSelf != true) _portal.SetActive(true);
        }

        else if (_currentLevel != level)
        {
            SceneManager.UnloadSceneAsync(_currentLevel.Level);
            asyncLoad = SceneManager.LoadSceneAsync(level.Level, LoadSceneMode.Additive);
            _currentLevel = level;
            _currentState = LevelState.LevelLoaded;
            if (!_particleSystem.gameObject.activeSelf) _particleSystem.gameObject.SetActive(true);
            if (_portal.activeSelf != true) _portal.SetActive(true);
        }
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        var scene = SceneManager.GetSceneByBuildIndex(level.Level);
        var sceneObjects = scene.GetRootGameObjects();
        Debug.Log("Scene root count: " + SceneManager.GetSceneAt(1).rootCount);
        Debug.Log("Scene object count: " + sceneObjects.Length);
        _player.GetJobs(sceneObjects);
        yield return null;
    }

    private IEnumerator UnloadScene(LevelItem level)
    {
        if (_currentLevel)
        {
            SceneManager.UnloadSceneAsync(_currentLevel.Level);
            if (_particleSystem.gameObject.activeSelf) _particleSystem.gameObject.SetActive(false);
            if (_portal.activeSelf) _portal.SetActive(false);
            _currentState = LevelState.Idle;
            _currentLevel = null;
        }

        yield return null;
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
                var particleSystemMain = _particleSystem.main;
                particleSystemMain.startColor = _currentLevel.ParticleColour;
            }
            _currentLevel.gameObject.transform.Rotate(0, _speed * Time.deltaTime, 0);
        }
    }
}
