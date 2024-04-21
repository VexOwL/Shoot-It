using System.Collections;
using UnityEngine;

public class EnemyShooter : Enemy
{
    [SerializeField] private float _fireDelay = 0.6f, _bulletSpeed = 5;
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

        if (_isAllowedToShoot && IsAlive && PlayerIsAlive)
        {
            BulletsPool.CreateBullet(_gun.position, _bulletSpeed);

            _isAllowedToShoot = false;
            StartCoroutine(Shoot());
        }
    }

    private void FixedUpdate()
    {
        if (IsAlive)
            Move();
    }

    public override void Move()
    {
        _position = transform.position;

        transform.right = Player.Instance.PlayerPosition - _position;

        _backDirection = (_position - Player.Instance.PlayerPosition).normalized;
        _playerDirection = (Player.Instance.PlayerPosition - _position).normalized;
        float distance = Vector2.Distance(Player.Instance.PlayerPosition, transform.position);

        if (distance > 8)
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
