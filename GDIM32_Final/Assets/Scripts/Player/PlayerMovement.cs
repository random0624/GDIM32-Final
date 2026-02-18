using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    //Initiate event for when an item is picked up
    //public delegate void ItemPickedUp(CollectableData itemData);
    //public event ItemPickedUp OnItemPickedUp;
    [SerializeField] private NavMeshAgent _player;

    private float _speed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float vertical = Input.GetAxis("Vertical");
        float horiztonal = Input.GetAxis("Horizontal");

        transform.Translate(((vertical * Vector3.forward) + (horiztonal * Vector3.right)) * _speed * Time.deltaTime);
    }
}
