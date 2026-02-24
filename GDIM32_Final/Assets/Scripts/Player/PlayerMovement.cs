using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private Collectable _nearbyCollectable;

    public void SetNearbyCollectable(Collectable collectable)
    {
        _nearbyCollectable = collectable;
    }

    //[SerializeField] private NavMeshAgent _player;
    [SerializeField] private Rigidbody  rb;

    [SerializeField] private float _speed;

    [SerializeField] private Transform orientation;
    [SerializeField] private Vector3 lookDirection;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Pickup item if it is nearby
        if (Input.GetKeyDown(KeyCode.E) && _nearbyCollectable != null)
        {
            _nearbyCollectable.PickUp();
            _nearbyCollectable = null;
        }
        HandleMovement();
    }

    private void HandleMovement()
    {
        
        float vertical = Input.GetAxis("Vertical");
        float horiztonal = Input.GetAxis("Horizontal");

        lookDirection = orientation.forward + orientation.right;
        transform.Translate(((vertical * orientation.forward) + (horiztonal * orientation.right)) * _speed * Time.deltaTime);

        /*
        if (Input.GetKey(KeyCode.W))
        { 
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.S))
        {

        }
        if(Input.GetKey(KeyCode.A))
        {

        }
        if(Input.GetKey(KeyCode.D))
        {

        }

        */
    }
}
