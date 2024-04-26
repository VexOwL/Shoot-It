using UnityEngine;
using UnityEngine.SceneManagement;

public class System_Input : MonoBehaviour
{
    [SerializeField] private GameObject _pauseScreen, _creditScreen;
    private Loader.Scene _currentScene;
    private InputSystem InputSystem;
    private bool _creditsShown = false;

    private void Awake()
    {
        InputSystem = new InputSystem();
        InputSystem.System.Enable();

        _pauseScreen.SetActive(false);
        _creditScreen.SetActive(false);
    }

    private void Start()
    {
        InputSystem.System.Restart.performed += Input_Restart;
        InputSystem.System.Continue.performed += Input_Continue;
        InputSystem.System.Pause.performed += Input_Pause;

        Time.timeScale = 1;

        _currentScene = Loader.GetCurrentScene();
    }

    private void Input_Restart(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        string currentScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(currentScene);
    }

    private void Input_Pause(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            _pauseScreen.SetActive(true);
        }
        else
        {
            _pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }

        if (_creditsShown)
        {
            Loader.Instance.LoadScene(Loader.Scene.MainMenu);
        }
    }

    private void Input_Continue(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (LevelManager.Instance.LevelFinished)
        {
            switch (_currentScene)
            {
                case Loader.Scene.Level1:
                    Loader.Instance.LoadScene(Loader.Scene.Level2);
                    break;

                case Loader.Scene.Level2:
                    Loader.Instance.LoadScene(Loader.Scene.Level3);
                    break;

                case Loader.Scene.Level3:
                    Loader.Instance.LoadScene(Loader.Scene.Level4);
                    break;

                case Loader.Scene.Level4:
                    Loader.Instance.LoadScene(Loader.Scene.Level5);
                    break;

                case Loader.Scene.Level5:
                    _creditScreen.SetActive(true);
                    _creditsShown = true;
                    break;
            }
        }
    }

    private void OnDisable()
    {
        InputSystem.System.Restart.performed -= Input_Restart;
        InputSystem.System.Continue.performed -= Input_Continue;
        InputSystem.System.Pause.performed -= Input_Pause;
    }
}
