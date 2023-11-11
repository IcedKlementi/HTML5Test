using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private Vector2 playerSpeed;

    private Vector2 _inputVector;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _inputVector.x = Input.GetAxisRaw("Horizontal");
        _inputVector.y = Input.GetAxisRaw("Vertical");
        
        DoMove();
    }

    private void DoMove()
    {
        var moveVector = Vector2.Scale(_inputVector.normalized, playerSpeed);
        _rb.velocity = moveVector;
    }
}