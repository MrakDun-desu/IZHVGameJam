using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    [SerializeField] int _offset = 50;
    [SerializeField] private int _multiplier = 2;
    [SerializeField] AudioMixer _mixer;
    [SerializeField] string _parameterName;
    [SerializeField] TextMeshProUGUI _valueLabel;
    [SerializeField] Slider _slider;

    private void OnValidate()
    {
        if (_valueLabel is null) return;
        if (_slider is null) return;
        _mixer.GetFloat(_parameterName, out var sliderValue);
        _slider.value = sliderValue;
        _valueLabel.text = (((int)sliderValue + _offset) * _multiplier).ToString();
    }

    public void SetVolume(float value)
    {
        _mixer.SetFloat(_parameterName, value);
        _valueLabel.text = (((int)value + _offset) * _multiplier).ToString();
    }

}
