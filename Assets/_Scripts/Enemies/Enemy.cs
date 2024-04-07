using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float _speed = 0.1f, _health = 5;
    [NonSerialized] public bool IsAlive = false;
    [NonSerialized] public Rigidbody2D EnemyRb;
    private Vector2 _enemyPosition, _moveVector;
    public float HealthCurrent {get; private set;}
    public float HealthMax {get; private set;}
    public float Speed {get; private set;}

    public virtual void Start()
    {
        Speed = _speed;
        HealthMax = _health;
        HealthCurrent = _health;
    }
    
    private void FixedUpdate()
    {
        if (IsAlive)
            Move();
    }

    public virtual void Update()
    {
        if (IsAlive)
        {
            if (HealthCurrent <= 0)
            {
                IsAlive = false;
            }
        }
    }

    public virtual void Move()
    {
        _enemyPosition = transform.position;
        _moveVector = (Player.PlayerPosition - _enemyPosition).normalized;
        EnemyRb.AddForce(_moveVector * _speed, ForceMode2D.Impulse);
    }

    public void ReceiveDamage()
    {
        if(HealthCurrent > 0)
            HealthCurrent--;
    }
}
