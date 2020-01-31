using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrikerTimerController : MonoBehaviour
{
    private enum SliderState
    {
        Up,
        Down
    }

    [Header("Thresholds")]
    [SerializeField] private List<Color> _thresholdColours;
    [HideInInspector] public List<float> thresholdValues;

    [Header("Slider")]
    [SerializeField] private Slider _sliderBar;
    [SerializeField] private Image _fill;
    [HideInInspector] public float speed;
    private SliderState currentState;

    void Start()
    {
        _sliderBar.value = 0;
        currentState = SliderState.Up;
    }


    void Update()
    {
        CheckThreshold();

        switch (currentState)
        {
            case SliderState.Up:
                SlideOnUp();
                break;
            case SliderState.Down:
                SlideOnDown();
                break;
            default:
                break;
        }
    }

    private void SlideOnUp()
    {
        _sliderBar.value += Time.deltaTime * speed;
        if (_sliderBar.value >= _sliderBar.maxValue) currentState = SliderState.Down;
    }

    private void SlideOnDown()
    {
        _sliderBar.value -= Time.deltaTime * speed;
        if (_sliderBar.value <= _sliderBar.minValue) currentState = SliderState.Up;
    }

    private void CheckThreshold()
    {
        for (int i = 0; i < thresholdValues.Count - 1; i++)
        {
            if (i > _thresholdColours.Count) break;

            if (_sliderBar.value < thresholdValues[i])
            {
                _fill.color = _thresholdColours[i];
                break;
            }
        }


        //if (_sliderBar.value < thresholdValues[0])
        //    _fill.color = _thresholdColours[0];

        //else if (_sliderBar.value < thresholdValues[1])
        //    _fill.color = _thresholdColours[1];

        //else if (_sliderBar.value < thresholdValues[2])
        //    _fill.color = _thresholdColours[2];

        //else if (_sliderBar.value < thresholdValues[3])
        //    _fill.color = _thresholdColours[3];
    }
}
