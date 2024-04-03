using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private InputSystem InputSystem;

    private void Awake()
    {
        InputSystem = new InputSystem();
    }

    private void Start()
    {
        //InputSystem.Player.
    }
}
