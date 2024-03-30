using UnityEngine;

public class BulletsPool : MonoBehaviour
{
    [SerializeField] private int _size = 10;
    [SerializeField] private bool _autoExpand = true;
    [SerializeField] private Bullet _prefab;
    private PoolMono<Bullet> _pool;
    public static BulletsPool Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _pool = new PoolMono<Bullet>(_prefab, _size);
        _pool.AutoExpand = _autoExpand;
    }

    public void CreateBullet()
    {
        var bullet = _pool.GetFreeElement();
    }
}
