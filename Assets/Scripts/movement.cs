using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D rigidbody2D;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(joystick.Horizontal * _speed, joystick.Vertical * _speed) ;
    }
}
