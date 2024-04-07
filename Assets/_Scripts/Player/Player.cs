using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private Player _player;
    public static Vector2 PlayerPosition;
    public static event EventHandler Player_Dead;

    private void Update()
    {
        PlayerPosition = transform.position;
    }

    public void ReceiveDamage()
    {
        Player_Dead?.Invoke(this, EventArgs.Empty);
        Debug.Log("Dead");
    }
}
