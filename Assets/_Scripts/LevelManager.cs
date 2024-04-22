using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _deathScreen, _victoryScreen;
    [NonSerialized] public bool LevelFinished = false;
    public float EnemiesCount = 0;
    private int _currentScene, _levelsFinished = 0;
    public static LevelManager Instance;

    private void Awake()
    {
        Instance = this;

        _deathScreen.SetActive(false);

        _levelsFinished = PlayerPrefs.GetInt(String.Levels_Finished);
    }

    private void Start()
    {
        Player.Instance.Player_Dead += Player_Dead;

        _currentScene = SceneManager.GetActiveScene().buildIndex;
        LevelFinished = false;
    }

    private void Update()
    {
        if (EnemiesCount <= 0)
        {
            _victoryScreen.SetActive(true);
            LevelFinished = true;

            if (_currentScene > _levelsFinished)
            {
                PlayerPrefs.SetInt(String.Levels_Finished, _currentScene);
                PlayerPrefs.Save();
            }
        }
    }

    private void Player_Dead(object sender, EventArgs e)
    {
        if (!LevelFinished)
        {
            _deathScreen.SetActive(true);
        }
    }

    private void OnDisable()
    {
        Player.Instance.Player_Dead -= Player_Dead;
    }
}
