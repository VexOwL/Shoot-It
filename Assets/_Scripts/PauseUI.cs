using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject _options;

    public void Load_MainMenu()
    {
        Loader.Instance.LoadScene(Loader.Scene.MainMenu);
    }

    public void Show_Options()
    {
        _options.SetActive(true);
    }
}
