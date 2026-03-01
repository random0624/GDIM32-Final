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


    [SerializeField] private GameObject _meat;
    [SerializeField] private int _meatCount;
    private Inventory _inventory;
    [SerializeField] private float _meatThrowForce;
    private Vector3 _meatSpawnPoint;

    
    [SerializeField] private Transform orientation;
    [SerializeField] private Vector3 lookDirection;
    
    void Start()
    {
        _inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        
        if(_inventory != null)
        {
            _meatCount=_inventory.CountItem(itemType.Meat);
        }


        if (Input.GetKeyDown(KeyCode.Space) && _meatCount != 0)
        {
            MeatThrow();
        }
    }

    public void TryPickUpNearby()
    {
        if (_nearbyCollectable == null) return;

        Inventory inv = GetComponent<Inventory>();
        if (inv == null) return;

        inv.AddItem(_nearbyCollectable.ItemData);
        Destroy(_nearbyCollectable.gameObject);
        _nearbyCollectable = null;
    }

    private void HandleMovement()
    {
        
        float vertical = Input.GetAxis("Vertical");
        float horiztonal = Input.GetAxis("Horizontal");

        lookDirection = orientation.forward + orientation.right;
        transform.Translate(((vertical * orientation.forward) + (horiztonal * orientation.right)) * _speed * Time.deltaTime);

    }

    private void MeatThrow()
    {
        _meatSpawnPoint = transform.position + transform.forward * 1.5f + Vector3.up;
        GameObject meat = Instantiate(_meat,_meatSpawnPoint, Quaternion.identity);

        if (meat != null)
        {
            Rigidbody rb = meat.GetComponent<Rigidbody>();

            Vector3 throwDirection = transform.forward + Vector3.up * 0.3f;
            rb.AddForce(throwDirection.normalized * _meatThrowForce, ForceMode.Impulse);
        }

     
    }
}
