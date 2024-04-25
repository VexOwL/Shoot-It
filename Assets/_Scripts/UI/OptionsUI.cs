using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider, _soundSlider;
    [SerializeField] private GameObject _options;
    public static OptionsUI Instance;
    public static event EventHandler ButtonPressed;

    public event EventHandler<MusicVolumeChangedEventArgs> MusicVolumeChanged;
    public class MusicVolumeChangedEventArgs : EventArgs
    {
        public float MusicVolume;
    }

    public event EventHandler<SoundVolumeChangedEventArgs> SoundVolumeChanged;
    public class SoundVolumeChangedEventArgs : EventArgs
    {
        public float SoundVolume;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _musicSlider.value = PlayerPrefs.GetFloat(String.Music_Volume, 0.2f) * 10;
        _soundSlider.value = PlayerPrefs.GetFloat(String.Sound_Volume, 0.5f) * 10;

        _options.SetActive(false);
        gameObject.SetActive(true);
    }

    public void Fullscreen_Toggle()
    {
        Screen.fullScreen = !Screen.fullScreen;

        if(Screen.fullScreen)
        {
            PlayerPrefs.SetInt(String.Fullscreen_On, 1);
        }
        else
        {
            PlayerPrefs.SetInt(String.Fullscreen_On, 0);
        }

        PlayerPrefs.Save();

        ButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    public void Options_Exit()
    {
        _options.SetActive(false);

        ButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    public void Delete_PlayerPrefs()
    {
        PlayerPrefs.DeleteAll();

        ButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    public void Update_MusicVolume()
    {
        float musicVolume = _musicSlider.value / 10;

        PlayerPrefs.SetFloat(String.Music_Volume, musicVolume);
        PlayerPrefs.Save();
        MusicVolumeChanged?.Invoke(this, new MusicVolumeChangedEventArgs { MusicVolume = musicVolume });
    }

    public void Update_SoundVolume()
    {
        float soundVolume = _soundSlider.value / 10;

        PlayerPrefs.SetFloat(String.Sound_Volume, soundVolume);
        PlayerPrefs.Save();
        SoundVolumeChanged?.Invoke(this, new SoundVolumeChangedEventArgs { SoundVolume = soundVolume });
    }

    public void Button_PlaySound()
    {
        ButtonPressed?.Invoke(this, EventArgs.Empty);
    }
}
