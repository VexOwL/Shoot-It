using System;
using System.Collections;
using UnityEngine;

public class EnemyShooter : Enemy
{
    [SerializeField] private float _fireDelay = 0.6f;
    [SerializeField] private Transform _gun;
    private Vector2 _backDirection, _playerDirection, _position;
    private bool _isAllowedToShoot = true;
    private PolygonCollider2D Collider;
    private BulletsPool BulletsPool;

    private void Awake()
    {
        EnemyRb = GetComponent<Rigidbody2D>();
        Collider = GetComponent<PolygonCollider2D>();

        BulletsPool = GetComponent<BulletsPool>();
    }

    public override void Update()
    {
        base.Update();

        Collider.enabled = IsAlive;

        if (_isAllowedToShoot && IsAlive)
        {
            BulletsPool.CreateBullet(_gun.position);

            _isAllowedToShoot = false;
            StartCoroutine(Shoot());
        }
    }

    public override void Move()
    {
        transform.right = Player.PlayerPosition - _position;

        _position = transform.position;
        _backDirection = (_position - Player.PlayerPosition).normalized;
        _playerDirection = (Player.PlayerPosition - _position).normalized;
        float distance = Vector2.Distance(Player.PlayerPosition, transform.position);

        if(distance > 8)
        {
            EnemyRb.AddForce(_playerDirection * Speed);
        }
        else
        {
            EnemyRb.AddForce(_backDirection * Speed);
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(_fireDelay);
        _isAllowedToShoot = true;
    }
}
