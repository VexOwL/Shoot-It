using System.Collections;
using UnityEngine;

public class EnemyBossEye : Enemy
{
    private PolygonCollider2D Collider;
    private bool _dashAllowed = true;
    [SerializeField] private float _dashDelay = 1.5f;

    private void Awake()
    {
        EnemyRb = GetComponent<Rigidbody2D>();
        Collider = GetComponent<PolygonCollider2D>();

        Collider.enabled = false;
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

    public override void Move()
    {
        Vector2 enemyPosition = transform.position;

        transform.right = Player.Instance.PlayerPosition - enemyPosition;

        if (_dashAllowed)
        {
            EnemyRb.AddForce(transform.right * Speed, ForceMode2D.Impulse);
            _dashAllowed = false;

            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        yield return new WaitForSeconds(_dashDelay);
        _dashAllowed = true;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        float pushForce = 3;

        if (other.gameObject.GetComponent<Player>())
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.ReceiveDamage();
        }
        else
        {
            EnemyRb.AddForce((transform.position - other.transform.position) * pushForce, ForceMode2D.Impulse);
        }
    }
}
