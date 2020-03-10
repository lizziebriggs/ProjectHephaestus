using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrikerTimerController : MonoBehaviour
{
    public enum SliderState
    {
        Up,
        Down,
        Idle
    }

    [Header("Thresholds")]
    [SerializeField] private List<Color> _thresholdColours;
    [HideInInspector] public List<float> thresholdValues;

    [Header("Slider")]
    [SerializeField] private Slider _sliderBar;
    [SerializeField] private Image _fill;
    [HideInInspector] public float speed;
    [HideInInspector] public SliderState currentState;

    public Slider SliderBar => _sliderBar;
    public Color Red => _thresholdColours[0];
    public Color Amber => _thresholdColours[1];
    public Color Green => _thresholdColours[2];
    public Color Blue => _thresholdColours[3];
    public Image Fill => _fill;

    void Start()
    {
        _sliderBar.value = 0;
        currentState = SliderState.Idle;
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
            case SliderState.Idle:
                IdleState();
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

    private void IdleState()
    {
        thresholdValues.Clear();
        _sliderBar.value = 0;
        currentState = SliderState.Up;
    }

    private void CheckThreshold()
    {
        for (int i = 0; i < thresholdValues.Count; i++)
        {
            if (i > _thresholdColours.Count) break;

            if (_sliderBar.value < thresholdValues[i])
            {
                _fill.color = _thresholdColours[i];
                break;
            }
        }
    }
}
