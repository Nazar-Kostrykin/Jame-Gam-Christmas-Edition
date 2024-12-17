using Narry;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;

    private Vector2 _moveDirection;

    void Start()
    {
        inputReader.MoveEvent += HandeMove;
    }


    void Update()
    {
        Move();
    }


    private void Move()
    {
        if(_moveDirection == Vector2.zero) 
            return;
        transform.Rotate(0,30,0);
    }

    private void HandeMove(Vector2 dir)
    {
        _moveDirection = dir;
    }
}