using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private InputSystem InputSystem;

    private void Awake()
    {
        InputSystem = new InputSystem();
        InputSystem.Player.Enable();
    }

    private void Start()
    {
        InputSystem.Player.Restart.performed += Input_Restart;
    }

    private void Input_Restart(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
}
