using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class SoundsPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _click, _levelChange, _enemyDeath, _playerDeath, _gunShot;
    private AudioSource AudioSource;
    public static SoundsPlayer Instance;

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
    }

    private void Start()
    {
        MainMenu.ButtonPressed += Button_Pressed;
        OptionsUI.ButtonPressed += Button_Pressed;
        PauseUI.ButtonPressed += Button_Pressed;
        Enemy.EnemyDead += Enemy_Dead;
        PlayerShooting.PlayerShot += Player_Shot;
        Player.PlayerDead += Player_Dead;

        Loader.Instance.SceneLoading += Scene_Loading;
        SceneManager.activeSceneChanged += Scene_Changed;
        OptionsUI.Instance.SoundVolumeChanged += SoundVolume_Changed;

        AudioSource.volume = PlayerPrefs.GetFloat(String.Sound_Volume, 0.5f);
    }

    private void Button_Pressed(object sender, EventArgs e)
    {
        AudioSource.PlayOneShot(_click, 1.5f);
    }

    private void Scene_Loading(object sender, Loader.SceneChangedEventArgs e)
    {
        AudioSource.PlayOneShot(_levelChange, 1.25f);
    }

    private void Player_Shot(object sender, EventArgs e)
    {
        AudioSource.PlayOneShot(_gunShot, 0.4f);
    }

    private void Enemy_Dead(object sender, EventArgs e)
    {
        AudioSource.PlayOneShot(_enemyDeath, 0.75f);
    }

    private void Player_Dead(object sender, EventArgs e)
    {
        AudioSource.PlayOneShot(_playerDeath, 1.25f);
    }

    private void SoundVolume_Changed(object sender, OptionsUI.SoundVolumeChangedEventArgs eventArgs)
    {
        AudioSource.volume = eventArgs.SoundVolume;
    }

    private void Scene_Changed(Scene arg0, Scene arg1)
    {
        Update_Events();
    }

    private void Update_Events()
    {
        MainMenu.ButtonPressed -= Button_Pressed;
        OptionsUI.ButtonPressed -= Button_Pressed;
        PauseUI.ButtonPressed -= Button_Pressed;
        Enemy.EnemyDead -= Enemy_Dead;
        PlayerShooting.PlayerShot -= Player_Shot;
        Player.PlayerDead -= Player_Dead;
        OptionsUI.Instance.SoundVolumeChanged -= SoundVolume_Changed;

        MainMenu.ButtonPressed += Button_Pressed;
        OptionsUI.ButtonPressed += Button_Pressed;
        PauseUI.ButtonPressed += Button_Pressed;
        Enemy.EnemyDead += Enemy_Dead;
        PlayerShooting.PlayerShot += Player_Shot;
        Player.PlayerDead += Player_Dead;
        OptionsUI.Instance.SoundVolumeChanged -= SoundVolume_Changed;
    }
}
