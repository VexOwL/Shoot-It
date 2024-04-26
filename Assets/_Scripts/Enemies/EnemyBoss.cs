using System.Collections;
using UnityEngine;

public class EnemyBoss : Enemy
{
    [SerializeField] private float _timeOnPostion = 5;
    [SerializeField] private Transform _pointer;
    [SerializeField] private Vector2[] _positions = new Vector2[4];
    private int _nextPosition = 0;
    private bool _moveAllowed = true;
    private Vector2 _position;

    private void FixedUpdate()
    {
        if(IsAlive)
        {
            Move();
        }
    }

    public override void Move()
    {
        _pointer.transform.right = Player.Instance.PlayerPosition - _position;

        if (_moveAllowed)
        {
            transform.position = Vector3.MoveTowards(transform.position, _positions[_nextPosition], Speed * Time.fixedDeltaTime);
            _position = transform.position;

            if (_position == _positions[_nextPosition])
            {
                StartCoroutine(OnPosition());
            }
        }
    }

    IEnumerator OnPosition()
    {
        _moveAllowed = false;

        yield return new WaitForSeconds(_timeOnPostion);

        _nextPosition = Random.Range(0, _positions.Length);

        _moveAllowed = true;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.ReceiveDamage();
        }
    }
}