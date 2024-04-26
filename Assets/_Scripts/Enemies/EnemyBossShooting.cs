using System.Collections;
using UnityEngine;

public class EnemyBossShooting : MonoBehaviour
{
    [SerializeField] private Enemy _boss;
    [SerializeField] private float _fireDelay, _bulletSpeed;
    [SerializeField] private Transform[] _guns;
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
            foreach (var gun in _guns)
            {
                BulletsPool.CreateBullet(gun.position, _bulletSpeed);
            }

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
