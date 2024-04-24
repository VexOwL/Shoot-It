using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _mainMenu;
    [SerializeField] private AudioClip _enemies;
    [SerializeField] private AudioClip _boss;
    private AudioSource AudioSource;
    private AudioClip _currentTrack;
    public static MusicPlayer Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        AudioSource = GetComponent<AudioSource>();

        ChangeMusic(_mainMenu);
    }

    private void Start()
    {
        Loader.Instance.SceneLoading += Scene_Loading;
        OptionsUI.Instance.MusicVolumeChanged += MusicVolume_Changed;
        SceneManager.activeSceneChanged += Scene_Changed;

        AudioSource.volume = PlayerPrefs.GetFloat(String.Music_Volume, 0.2f);
    }

    private void Scene_Changed(Scene arg0, Scene arg1)
    {
        Update_Events();
    }

    private void MusicVolume_Changed(object sender, OptionsUI.MusicVolumeChangedEventArgs eventArgs)
    {
        AudioSource.volume = eventArgs.MusicVolume;
    }

    private void Scene_Loading(object sender, Loader.SceneChangedEventArgs eventArgs)
    {   
        Loader.Scene currentScene = eventArgs.TargetScene;

        if (currentScene == Loader.Scene.MainMenu)
        {
            ChangeMusic(_mainMenu);
        }

        if (currentScene == Loader.Scene.Level5)
        {
            ChangeMusic(_boss);
        }

        if (currentScene == Loader.Scene.Level1 || currentScene == Loader.Scene.Level2 || currentScene == Loader.Scene.Level3 || currentScene == Loader.Scene.Level4)
        {
            ChangeMusic(_enemies);
        }
    }

    private void ChangeMusic(AudioClip music)
    {
        if (_currentTrack != music)
        {
            AudioSource.clip = music;
            AudioSource.Play();
            _currentTrack = music;
        }
    }

    private void Update_Events()
    {
        OptionsUI.Instance.MusicVolumeChanged -= MusicVolume_Changed;
        OptionsUI.Instance.MusicVolumeChanged += MusicVolume_Changed;
    }
}
