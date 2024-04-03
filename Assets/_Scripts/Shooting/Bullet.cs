using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 2;
    [SerializeField] private Transform _bulletVisual;
    [SerializeField] private LayerMask _capsuleCastLayer = 0;
    private Vector2 _castDirection = new Vector2(0, 0), _bulletSize;
    private bool _impacted;

    private void Start()
    {
        _bulletSize = new Vector2(_bulletVisual.localScale.x, _bulletVisual.localScale.y);
    }

    private void FixedUpdate()
    {
        transform.Translate(_speed * Time.fixedDeltaTime, 0, 0);

        RaycastHit2D castHit = Physics2D.CapsuleCast(transform.position, _bulletSize, CapsuleDirection2D.Horizontal, transform.rotation.z, _castDirection, _capsuleCastLayer);
        Collider2D hitCollider = castHit.collider;

        if (hitCollider != null)
        {
            if(hitCollider.TryGetComponent(out IDamagable enemy))
            {
                enemy.ReceiveDamage();
            }

            gameObject.SetActive(false);
            BulletsPool.Instance.ReturnBullet(this);
        }
    }
}