using System;
using Narry;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputReader _input;

    [SerializeField] private Rigidbody _playerRB;
    [SerializeField] private float _turnSpeed = 10f; 
    [SerializeField] private float _maxTurnAngle = 30f;  
    [SerializeField] private float _speed;  
    
    [SerializeField] private LayerMask _groundLayer;
    private Vector3 _moveDirection = Vector3.zero;

    private float currentRotation = 0f; 
    private float turnInput = 0f;

    private void Start()
    {
        _playerRB = gameObject.GetComponent<Rigidbody>();
        InputReader.MoveEvent += SetTurnInput;
    }

    public void  SetTurnInput(Vector2 moveInput)
    {
        turnInput = moveInput.x;
    }

    private void Update()
    {// Обчислюємо кут повороту, обмежений maxTurnAngle
        float turnAmount = (turnInput * -1 )* _turnSpeed * Time.deltaTime;

        // Отримуємо поточний кут повороту по осі Y
        float currentRotationY = transform.eulerAngles.y;

        // Додаємо кут повороту до поточного кута
        float newRotationY = currentRotationY + turnAmount;

        // Обмежуємо кут повороту на 30 градусів в обидва боки
        if (newRotationY > 180) newRotationY -= 360; // Коригуємо для негативних значень
        newRotationY = Mathf.Clamp(newRotationY, -_maxTurnAngle, _maxTurnAngle);

        // Повертаємо сани тільки по осі Y
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, newRotationY, transform.eulerAngles.z);
    }

    void FixedUpdate()
    {
        // Рух санок вниз по схилу (незалежно від інпуту)
        Vector3 forwardMovement = transform.forward * (_speed * Time.fixedDeltaTime);
        _playerRB.MovePosition(_playerRB.position + forwardMovement);
    }
    
}