using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Slider MusicSlider;
    public Slider SFXSlider;
    public AudioMixer mixer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        MusicSlider.value = 1;
        SFXSlider.value = 1;

        SetMusicVolume(MusicSlider.value);
        SetFXVolume(SFXSlider.value);

        MusicSlider.onValueChanged.AddListener(SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(SetFXVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMusicVolume(float value)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }

    public void SetFXVolume(float value)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log(value) * 20);
    }
}
