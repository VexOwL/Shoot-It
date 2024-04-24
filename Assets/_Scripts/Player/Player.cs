using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public Vector2 PlayerPosition { get; private set; }
    public static event EventHandler PlayerDead;
    public static Player Instance;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        PlayerPosition = transform.position;
    }

    public void ReceiveDamage()
    {
        PlayerDead?.Invoke(this, EventArgs.Empty);
        gameObject.SetActive(false);
    }
}
