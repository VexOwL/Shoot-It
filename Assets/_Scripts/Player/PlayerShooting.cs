using System;
using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private float _fireDelay = 0.2f;
    private InputSystem InputSystem;
    private bool _isShooting, _isAllowedToShoot = true;

    private void Awake()
    {
        InputSystem = new InputSystem();
        InputSystem.Player.Enable();
    }

    private void Update()
    {
        _isShooting = Convert.ToBoolean(InputSystem.Player.Shoot.ReadValue<float>());

        if (_isShooting && _isAllowedToShoot)
        {
            BulletsPool.Instance.CreateBullet();
            
            _isAllowedToShoot = false;
            StartCoroutine(FireDelay());
        }
    }

    IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(_fireDelay);
        _isAllowedToShoot = true;
    }
}
