using UnityEngine;

public class EnemyBasic : Enemy, IDamagable
{
    [SerializeField] private float _healthMax = 5;
    [SerializeField] private GameObject _enemyVisual, _enemyDeadVisual;
    private bool _isAlive = true;
    private float _health;
    private Vector3 _deadPosition = new Vector3(0, 0, 3);
    private BoxCollider2D Collider;

    public override void Awake()
    {
        base.Awake();
        Collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        _health = _healthMax;
    }

    private void FixedUpdate()
    {
        if (_isAlive)
            MoveToPlayer();
    }

    private void Update()
    {
        if (_health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        _isAlive = false;
        Collider.enabled = false;
        transform.position += _deadPosition;
        _enemyDeadVisual.SetActive(true);
        _enemyVisual.SetActive(false);
    }

    public void ReceiveDamage()
    {
        _health--;
        Debug.Log(_health);
        _enemyVisual.transform.localScale = new Vector3(_health / _healthMax, _health / _healthMax, _health / _healthMax);
    }
}
