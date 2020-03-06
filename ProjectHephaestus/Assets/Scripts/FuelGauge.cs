using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelGauge : MonoBehaviour
{
    [SerializeField] private FurnaceManager furnace;

    [Header("Slider")]
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _fill;
    [SerializeField] private int _sliderMaxValue;
    [SerializeField] private Color _minColor, _maxColor;

    void Start()
    {
        _slider.maxValue = _sliderMaxValue;
        _slider.value = _slider.minValue;
    }


    void Update()
    {
        ChangeColour();

        _slider.value = furnace.Fuel;
    }

    public void ChangeColour()
    {
        _fill.color = Color.Lerp(_minColor, _maxColor, furnace.Fuel / _slider.maxValue);
    }
}
