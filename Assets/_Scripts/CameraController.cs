using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform target; // Об'єкт, за яким буде слідувати камера (наприклад, персонаж)

    [Header("Camera Settings")]
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -10); // Відступ камери від об'єкта
    [SerializeField] private float smoothSpeed = 5f; // Швидкість плавного слідування

    private void LateUpdate()
    {

        // Розрахунок позиції камери з урахуванням відступу
        Vector3 desiredPosition = target.position + offset;

        // Плавний перехід камери до нової позиції
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Оновлення позиції камери
        transform.position = smoothedPosition;

        // Опціонально: спрямовуємо камеру на персонажа
        transform.LookAt(target);
    }
}