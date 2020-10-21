using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playerbehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    private Inputs inputs;
    private Vector2 direction;

    private void OnEnable()
    {
        inputs = new Inputs();
        inputs.Enable();
        inputs.player.move.performed += OnMovePerformed;
        inputs.player.move.canceled += OnMoveCanceled;
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        direction = Vector2.zero;
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        direction = obj.ReadValue<Vector2>();
        Debug.Log(direction);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        var myRigidBody = GetComponent<Rigidbody2D>();
        direction.y = 0;
        //myRigidBody.velocity = direction;
        if (myRigidBody.velocity.sqrMagnitude < maxSpeed)
        myRigidBody.AddForce(direction * speed);
    }
}
