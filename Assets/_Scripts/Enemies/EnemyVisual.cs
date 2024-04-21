using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] private Enemy Enemy;
    [SerializeField] private GameObject _enemyHealth;
    [SerializeField] private float _secondsToAppear = 1;
    private float _healthScale;
    private bool _isAppeared = false;
    private Vector3 _healthSize;

    private void Start()
    {
        _healthSize = _enemyHealth.transform.localScale;

        _enemyHealth.transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (_isAppeared == false)
        {
            if (_enemyHealth.transform.localScale.x < _healthSize.x && _enemyHealth.transform.localScale.y < _healthSize.y)
            {
                _enemyHealth.transform.localScale += new Vector3(Time.deltaTime / _secondsToAppear, Time.deltaTime / _secondsToAppear, 0);
            }
            else
            {
                Enemy.IsAlive = true;
                _isAppeared = true;
            }
        }
        else
        {
            VisualizeHealth();
        }

        if (Enemy.HealthCurrent <= 0)
        {
            DeathVisual();
        }
    }

    public void VisualizeHealth()
    {
        _healthScale = Enemy.HealthCurrent / Enemy.MaxHealth;

        _enemyHealth.transform.localScale = _healthSize * _healthScale;
    }

    public void DeathVisual()
    {
        Vector3 deadPosition = new Vector3(transform.position.x, transform.position.y, 3);
        transform.position = deadPosition;
    }
}