using System;
using Narry;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputReader _input;

    [SerializeField] private Rigidbody _playerRB;
    [SerializeField] private float _speed;
    
    public float acceleration = 0.5f;    // Прискорення
    public float deceleration = 0.3f;
    public float maxSpeed = 30f; 
    public float turnSpeed = 200f;     // Speed of turning
    public float maxRotationAngle = 30f;
    public float groundCheckDistance = 1.5f;
    
    [SerializeField, ReadOnly] private float _currentSpeed = 0f;
    private float currentRotationY = 0f;

    private Vector3 moveDirection; 
    
    private float _turnInput = 0f;

    private void Start()
    {
        _playerRB = gameObject.GetComponent<Rigidbody>();
        InputReader.MoveEvent += SetTurnInput;
    }

    public void  SetTurnInput(Vector2 moveInput)
    {
        _turnInput = moveInput.x;
    }

    private void Update()
    {
        float rotationDelta = _turnInput * turnSpeed * Time.fixedDeltaTime;

        currentRotationY = Mathf.Clamp(currentRotationY + rotationDelta, -maxRotationAngle, maxRotationAngle);

        // Встановлюємо обмежений кут на об'єкт
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, currentRotationY, transform.localEulerAngles.z);

        // Оновлюємо швидкість об'єкта
        UpdateSpeed();
        MoveObject();
    }

    private void MoveObject()
    {
        Vector3 moveDirection = transform.forward * _currentSpeed * Time.fixedDeltaTime;
        transform.position += moveDirection;
    }

    private void UpdateSpeed()
    {
        RaycastHit hit;

        // Виконуємо Raycast вниз для перевірки поверхні
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance))
        {
            // Якщо об'єкт на землі, прискорюємо його
            _currentSpeed = Mathf.Min(_currentSpeed + acceleration * Time.fixedDeltaTime, maxSpeed);
        }
        else
        {
            // Якщо об'єкт у повітрі або сповільнюється, зменшуємо швидкість
            _currentSpeed = Mathf.Max(_currentSpeed - deceleration * Time.fixedDeltaTime, 0f);
        }
    }

    
}