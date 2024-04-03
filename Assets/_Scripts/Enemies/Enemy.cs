using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 0.01f;
    private Vector2 _moveVector, _enemyPosition;
    private Rigidbody2D EnemyRb;

    public virtual void Awake()
    {
        EnemyRb = GetComponent<Rigidbody2D>();
    }

    public virtual void MoveToPlayer()
    {
        _enemyPosition = transform.position;
        _moveVector = Player.PlayerPosition - _enemyPosition;
        EnemyRb.AddForce(_moveVector * _speed, ForceMode2D.Impulse);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            Destroy(other.gameObject);
        }
    }
}
