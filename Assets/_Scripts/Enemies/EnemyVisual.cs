using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] Enemy _enemy;
    [SerializeField] private GameObject _enemyHealth;
    [SerializeField] private float _secondsToAppear = 1;
    private Vector3 _healthScale;
    private bool _isAppeared = false;

    private void Start()
    {
        _enemyHealth.transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (_isAppeared == false)
        {
            if (_enemyHealth.transform.localScale.x < 1 && _enemyHealth.transform.localScale.y < 1)
            {
                _enemyHealth.transform.localScale += new Vector3(Time.deltaTime / _secondsToAppear, Time.deltaTime / _secondsToAppear, 0);
            }
            else
            {
                _enemy.IsAlive = true;
                _isAppeared = true;
            }
        }
        else
        {
            VisualizeHealth();
        }

        if (_enemy.HealthCurrent <= 0)
        {
            DeathVisual();
        }
    }

    public void VisualizeHealth()
    {
        _healthScale = new Vector3(_enemy.HealthCurrent / _enemy.HealthMax, _enemy.HealthCurrent / _enemy.HealthMax, 0);
        _enemyHealth.transform.localScale = _healthScale;
    }

    public void DeathVisual()
    {
        Vector3 deadPosition = new Vector3(transform.position.x, transform.position.y, 3);
        transform.position = deadPosition;
    }
}
