using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Target _target;
    [NonSerialized] public float Speed = 1;
    private float _destroyDistance = 15;
    private Rigidbody2D rb;

    private enum Target { Player, Enemy }

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.right * Speed, ForceMode2D.Impulse);

        if (Vector3.Distance(Vector3.zero, transform.position) > _destroyDistance)
        {
            ReturnBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_target == Target.Enemy)
        {
            if (other.gameObject.GetComponent<Enemy>())
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();

                enemy.ReceiveDamage();
            }

            if (!other.gameObject.GetComponent<Player>() && !other.gameObject.GetComponent<Bullet>())
            {
                ReturnBullet();
            }
        }
        else if (_target == Target.Player)
        {
            if (other.gameObject.GetComponent<Player>())
            {
                Player player = other.gameObject.GetComponent<Player>();

                player.ReceiveDamage();
            }

            if (!other.gameObject.GetComponent<EnemyShooter>() && !other.gameObject.GetComponent<EnemyBoss>() && !other.gameObject.GetComponent<Bullet>())
            {
                ReturnBullet();
            }
        }

        if(other.gameObject.GetComponent<EnemyBasic>())
        {
            EnemyBasic enemy = other.gameObject.GetComponent<EnemyBasic>();

            float pushForce = 1;
            enemy.PushAway(transform.position, pushForce);
        }
    }

    private void ReturnBullet()
    {
        gameObject.SetActive(false);
        BulletsPool.Instance.ReturnBullet(this);
    }
}