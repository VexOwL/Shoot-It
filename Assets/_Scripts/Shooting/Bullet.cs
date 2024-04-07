using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 20;
    [SerializeField] private Transform _bulletVisual;
    [SerializeField] private LayerMask _layerMask;
    private Vector2 _castDirection = Vector2.zero, _bulletSize;
    private float _castDistance = 0;

    public void Start()
    {
        _bulletSize = _bulletVisual.localScale;
    }

    private void FixedUpdate()
    {
        transform.Translate(_speed * Time.fixedDeltaTime, 0, 0);
    }

    private void Update()
    {
        Raycast();
    }

    public void Raycast()
    {
        RaycastHit2D castHit = Physics2D.CapsuleCast(transform.position, _bulletSize, CapsuleDirection2D.Horizontal, transform.rotation.z, _castDirection, _castDistance, _layerMask);
        Collider2D hitCollider = castHit.collider;

        if (hitCollider != null)
        {
            if (hitCollider.TryGetComponent(out IDamageable target))
            {
                target.ReceiveDamage();
            }

            gameObject.SetActive(false);
            BulletsPool.Instance.ReturnBullet(this);
        }
    }
}