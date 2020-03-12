using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonObject : MonoBehaviour
{
    private SingletonObject _instance;
    public SingletonObject Instance => _instance;

    void Awake()
    {
        if (!_instance && _instance != this)
            Destroy(this.gameObject);
        else _instance = this;
    }
}
