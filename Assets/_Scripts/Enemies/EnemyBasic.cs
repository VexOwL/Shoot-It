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
        float pushForce = 5;

        if (other.gameObject.GetComponent<Player>())
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.ReceiveDamage();
        }
        else
        {
            PushAway(other.transform.position, pushForce);
        }
    }

    public void PushAway(Vector3 pushPoint, float pushForce)
    {

        EnemyRb.AddForce((transform.position - pushPoint) * pushForce, ForceMode2D.Impulse);
    }
}
