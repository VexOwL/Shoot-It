using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class SoundsPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _click, _levelChange, _enemyDeath, _playerDeath;
    //[SerializeField] private AudioClip[] _shotSounds;
    private AudioSource AudioSource;
    public static SoundsPlayer Instance;
    Random Random = new Random();

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
    }

    private void Player_Dead(object sender, EventArgs e)
    {
         AudioSource.PlayOneShot(_playerDeath);
    }

    private void Player_Shot(object sender, EventArgs e)
    {
        // int shot;
        // shot = Random.Next(0, _shotSounds.Length);
        
        // AudioSource.PlayOneShot(_shotSounds[shot]);
    }

    private void Scene_Loading(object sender, Loader.SceneChangedEventArgs e)
    {
        AudioSource.PlayOneShot(_levelChange);
    }

    private void Button_Pressed(object sender, EventArgs e)
    {
        AudioSource.PlayOneShot(_click);
    }

    private void Enemy_Dead(object sender, EventArgs e)
    {
        AudioSource.PlayOneShot(_enemyDeath);
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

        MainMenu.ButtonPressed += Button_Pressed;
        OptionsUI.ButtonPressed += Button_Pressed;
        PauseUI.ButtonPressed += Button_Pressed;
        Enemy.EnemyDead += Enemy_Dead;
        PlayerShooting.PlayerShot += Player_Shot;
        Player.PlayerDead += Player_Dead;
    }
}
