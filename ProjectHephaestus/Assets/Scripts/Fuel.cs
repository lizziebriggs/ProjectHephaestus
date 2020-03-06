using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private int fuelValue;
    public int FuelValue => fuelValue;

    public int fuelLeft;

    private bool _canBurn;
    private float _burnCountdown;
    private float _burnTimer;

    void Start()
    {
        fuelLeft = fuelValue;   
    }

    void Update()
    {
        if (_canBurn) Burn();
        if (fuelLeft <= 0) Destroy(gameObject);
    }

    public void StartBurning(float countdownValue)
    {
        _burnCountdown = countdownValue;
        _burnTimer = _burnCountdown;
        _canBurn = true;
    }

    private void Burn()
    {
        _burnTimer -= Time.deltaTime;

        if (_burnTimer > 0) return;

        fuelLeft--;
        _burnTimer = _burnCountdown;
    }
}
