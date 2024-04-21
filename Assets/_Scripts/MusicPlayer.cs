using System;
using UnityEngine;

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
        Loader.Instance.SceneChanged += Scene_Changed;
    }

    private void Scene_Changed(object sender, Loader.SceneChangedEventArgs eventArgs)
    {
        Loader.Scene currentScene = eventArgs.targetScene;

        Debug.Log($"MusicPlayer. Current Scene: {currentScene}");

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
}
