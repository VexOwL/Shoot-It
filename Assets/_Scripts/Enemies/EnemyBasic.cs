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

    
    public void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            Destroy(other.gameObject);
        }
    }
}
