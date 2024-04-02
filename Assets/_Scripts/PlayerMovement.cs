using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    private InputSystem InputSystem;
    private Rigidbody2D PlayerRb;
    private Vector2 _moveVector, _rotationVector;
    private Vector3 _pointerPositionDifference;
    private float _rotationAngle;

    private void Awake()
    {
        InputSystem = new InputSystem();
        InputSystem.Player.Enable();

        PlayerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _moveVector = InputSystem.Player.Movement.ReadValue<Vector2>().normalized;
        PlayerRb.AddForce(_moveVector * _speed);

        _rotationVector = InputSystem.Player.Rotation.ReadValue<Vector2>();
        Vector3 _pointerVector = new Vector3(_rotationVector.x, _rotationVector.y, 0);
        
        _pointerPositionDifference = Camera.main.ScreenToWorldPoint(_pointerVector) - transform.position;
        _rotationAngle = Mathf.Atan2(_pointerPositionDifference.y, _pointerPositionDifference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, _rotationAngle);
    }
}
