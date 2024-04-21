using System.Collections;
using UnityEngine;

public class EnemyBossShooting : MonoBehaviour
{
    [SerializeField] private Enemy _boss;
    [SerializeField] private float _fireDelay = 0.6f, _bulletSpeed = 15;
    [SerializeField] private Transform _gun;
    private bool _shootAllowed = true;
    private BulletsPool BulletsPool;

    private void Awake()
    {
        BulletsPool = GetComponent<BulletsPool>();
    }
    
    private void Update()
    {
        if (_shootAllowed && _boss.IsAlive && _boss.PlayerIsAlive)
        {
            BulletsPool.CreateBullet(_gun.position, _bulletSpeed);

            _shootAllowed = false;
            StartCoroutine(Shoot());
        }
    }
    
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(_fireDelay);
        _shootAllowed = true;
    }
}
