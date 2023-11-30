using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;
    private void Start()
    {
        GetVolume();
    }
    public void SetVolume(float sliderValue)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);

        // Save the slider value to PlayerPrefs
        PlayerPrefs.SetFloat("SliderValue", sliderValue);
    }

    public void GetVolume()
    {
        // Retrieve the slider value from PlayerPrefs
        float sliderValue = PlayerPrefs.GetFloat("SliderValue", 1f); // Default value is 1 if it doesn't exist
        slider.value = sliderValue;
    }
}