using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private InputSystem InputSystem;

    private void Awake()
    {
        InputSystem = new InputSystem();
        InputSystem.Player.Enable();
    }

    private void Update()
    {
        InputSystem.Player.Shoot.performed += Player_Shoot;
    }

    private void Player_Shoot(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        BulletsPool.Instance.CreateBullet();
    }
}
