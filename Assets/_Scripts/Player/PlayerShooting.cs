using System;
using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private float _fireDelay = 0.2f;
    [SerializeField] private Transform _gun;
    private InputSystem InputSystem;
    private BulletsPool BulletsPool;
    private bool _isShooting, _isAllowedToShoot = true;

    private void Awake()
    {
        InputSystem = new InputSystem();
        InputSystem.Player.Enable();

        BulletsPool = GetComponent<BulletsPool>();
    }

    private void Update()
    {
        _isShooting = Convert.ToBoolean(InputSystem.Player.Shoot.ReadValue<float>());

        if (_isShooting && _isAllowedToShoot)
        {
            BulletsPool.CreateBullet(_gun.position);

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
