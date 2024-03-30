using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody2D BulletRb;
    private Vector2 _moveVector = new Vector2(1, 0);

    private void Awake()
    {
        BulletRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        BulletRb.AddRelativeForce(_moveVector * _speed, ForceMode2D.Impulse);
    }
}
