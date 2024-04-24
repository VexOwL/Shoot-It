using System;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _options;
    [SerializeField] private GameObject[] _levelsLockedImages;
    private int _levelsFinished;
    public static event EventHandler ButtonPressed;

    private void Awake()
    {
        _options.SetActive(false);
    }

    private void Start()
    {
        _levelsFinished = PlayerPrefs.GetInt(String.Levels_Finished);

        for (int i = 0; i < _levelsFinished - 1; i++)
        {
            _levelsLockedImages[i].SetActive(false);
        }
    }

    public void Show_Options()
    {
        _options.SetActive(true);

        ButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    public void Quit_Game()
    {
        Application.Quit();

        ButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    public void Load_Level1()
    {
        Loader.Instance.LoadScene(Loader.Scene.Level1);

        ButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    public void Load_Level2()
    {
        if (_levelsFinished >= 1)
        {
            Loader.Instance.LoadScene(Loader.Scene.Level2);

            ButtonPressed?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Load_Level3()
    {
        if (_levelsFinished >= 2)
        {
            Loader.Instance.LoadScene(Loader.Scene.Level3);
        }

        ButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    public void Load_Level4()
    {
        if (_levelsFinished >= 3)
        {
            Loader.Instance.LoadScene(Loader.Scene.Level4);
        }

        ButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    public void Load_Level5()
    {
        if (_levelsFinished >= 4)
        {
            Loader.Instance.LoadScene(Loader.Scene.Level5);
        }

        ButtonPressed?.Invoke(this, EventArgs.Empty);
    }
}