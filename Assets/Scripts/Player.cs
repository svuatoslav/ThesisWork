using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    private Vector3 _direction = Vector3.zero;
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        _direction.z = Input.GetAxis("Vertical");
        _direction.x = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.E))
            _direction.y = 1;
        else if (Input.GetKey(KeyCode.Q))
            _direction.y = -1;
        else
            _direction.y = 0;
        var speed = _speed * Time.fixedDeltaTime * _direction;
        transform.Translate(speed);
    }
}
