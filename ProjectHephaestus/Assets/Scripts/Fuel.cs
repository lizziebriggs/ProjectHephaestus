using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private int fuelValue;
    [SerializeField, Range(0, 100)] private int _tempValue;
    public FurnaceManager Furnace { get; set; }

    public int TempValue => _tempValue;

    public int FuelValue => fuelValue;

    private int _fuelLeft;

    [HideInInspector] public bool _canBurn;
    private float _burnCountdown;
    private float _burnTimer;

    void Start()
    {
        _fuelLeft = fuelValue;   
    }

    void Update()
    {
        if (_canBurn) Burn();
        if (_fuelLeft <= 0)
        {
            Furnace._fuelCount.Remove(this);
            Furnace.TempValue -= TempValue;
            Destroy(gameObject);
        }
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

        _fuelLeft--;
        _burnTimer = _burnCountdown;
    }
}
