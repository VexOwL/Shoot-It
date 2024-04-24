using System;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject _options;
    public static event EventHandler ButtonPressed;

    public void Load_MainMenu()
    {
        Loader.Instance.LoadScene(Loader.Scene.MainMenu);

        ButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    public void Show_Options()
    {
        _options.SetActive(true);

        ButtonPressed?.Invoke(this, EventArgs.Empty);
    }
}
