using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Vector2 PlayerPosition;

    private void Update()
    {
        PlayerPosition = transform.position;
    }
}
