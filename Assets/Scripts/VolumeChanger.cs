using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    [SerializeField] int offset = 80;
    [SerializeField] AudioMixer mixer;
    [SerializeField] string parameterName;
    [SerializeField] TextMeshProUGUI valueLabel;
    [SerializeField] Slider slider;

    private void OnValidate()
    {
        if (valueLabel is null) return;
        if (slider is null) return;
        valueLabel.text = ((int)slider.value + offset).ToString();
    }

    public void SetVolume(float value)
    {
        mixer.SetFloat(parameterName, value);
        valueLabel.text = ((int)value + offset).ToString();
    }

}
