using UnityEngine;

public class EnemyBasic : Enemy
{
    private BoxCollider2D Collider;

    private void Awake()
    {
        EnemyRb = GetComponent<Rigidbody2D>();
        Collider = GetComponent<BoxCollider2D>();
    }

    public override void Update()
    {
        base.Update();

        Collider.enabled = IsAlive;
    }

    private void FixedUpdate()
    {
        if (IsAlive)
            Move();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        float pushForce = 2;

        if (other.gameObject.GetComponent<Player>())
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.ReceiveDamage();
        }
        else if (!other.gameObject.GetComponent<Bullet>())
        {
            EnemyRb.AddForce((transform.position - other.transform.position) * pushForce, ForceMode2D.Impulse);
        }
    }
}
