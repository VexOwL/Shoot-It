using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    public float Speed = 0.1f, MaxHealth = 5;
    [NonSerialized] public bool IsAlive = false;
    [NonSerialized] public Rigidbody2D EnemyRb;
    public bool PlayerIsAlive {get; private set;}
    public float HealthCurrent {get; private set;}
    private Vector2 _enemyPosition, _moveVector;
    public static event EventHandler EnemyDead;

    public virtual void Start()
    {
        HealthCurrent = MaxHealth;

        PlayerIsAlive = true;
        Player.PlayerDead += Player_Dead;

        LevelManager.Instance.EnemiesCount++;
    }

    private void Player_Dead(object sender, EventArgs e)
    {
        PlayerIsAlive = false;
    }

    public virtual void Update()
    {
        if (IsAlive)
        {
            if (HealthCurrent <= 0)
            {
                IsAlive = false;
                LevelManager.Instance.EnemiesCount--;
                EnemyDead?.Invoke(this, EventArgs.Empty);

            }
        }
    }

    public virtual void Move()
    {
        _enemyPosition = transform.position;
        _moveVector = (Player.Instance.PlayerPosition - _enemyPosition).normalized;
        EnemyRb.AddForce(_moveVector * Speed);
    }

    public void ReceiveDamage()
    {
        if(HealthCurrent > 0)
            HealthCurrent--;
    }

    private void OnDisable()
    {
        Player.PlayerDead -= Player_Dead;
    }
}
