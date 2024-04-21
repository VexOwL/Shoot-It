using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public enum Scene { MainMenu = 0, Level1 = 1, Level2 = 2, Level3 = 3, Level4 = 4, Level5 = 5 }

    public event EventHandler<SceneChangedEventArgs> SceneChanged;
    public class SceneChangedEventArgs : EventArgs
    {
        public Scene targetScene;
    }

    public static Loader Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(Scene targetScene)
    {
        SceneManager.LoadScene(targetScene.ToString());
        SceneChanged?.Invoke(this, new SceneChangedEventArgs {targetScene = targetScene});
        Debug.Log("Loader. Event");
    }

    public static Scene GetCurrentScene()
    {
        Scene currentscene = Scene.MainMenu;

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                currentscene = Scene.MainMenu;
                break;

            case 1:
                currentscene = Scene.Level1;
                break;

            case 2:
                currentscene = Scene.Level2;
                break;

            case 3:
                currentscene = Scene.Level3;
                break;

            case 4:
                currentscene = Scene.Level4;
                break;

            case 5:
                currentscene = Scene.Level5;
                break;
        }

        return currentscene;
    }
}