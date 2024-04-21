using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public Vector2 PlayerPosition { get; private set; }
    public event EventHandler Player_Dead;
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
        Player_Dead?.Invoke(this, EventArgs.Empty);
        gameObject.SetActive(false);
    }
}
