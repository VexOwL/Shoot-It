using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private GameObject _options;
    public static OptionsUI Instance;

    public event EventHandler<MusicVolumeChangedEventArgs> MusicVolumeChanged;

    public class MusicVolumeChangedEventArgs : EventArgs
    {
        public float MusicVolume;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Debug.Log($"PlayerPrefs. Music_Volume: {PlayerPrefs.GetFloat(String.Music_Volume, 0.2f)}");
        _musicSlider.value = PlayerPrefs.GetFloat(String.Music_Volume, 0.2f) * 10;

        _options.SetActive(false);
        gameObject.SetActive(true);
    }

    public void Fullscreen_Toggle()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void Options_Exit()
    {
        _options.SetActive(false);
    }

    public void Delete_PlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Update_MusicVolume()
    {
        float musicVolume = _musicSlider.value / 10;

        PlayerPrefs.SetFloat(String.Music_Volume, musicVolume);
        PlayerPrefs.Save();
        MusicVolumeChanged?.Invoke(this, new MusicVolumeChangedEventArgs { MusicVolume = musicVolume });

        Debug.Log($"OptionsUI. Volume: {musicVolume}");
    }
}
