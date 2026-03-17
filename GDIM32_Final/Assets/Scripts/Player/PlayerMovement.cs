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
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float _speed;

    [SerializeField] private GameObject _meat;
    [SerializeField] private int _meatCount;
    private Inventory _inventory;
    [SerializeField] private float _meatThrowForce;
    private Vector3 _meatSpawnPoint;

    [SerializeField] private Transform orientation;
    [SerializeField] private Vector3 lookDirection;

    [SerializeField] private Animator playerAnim;

    public delegate void LoseLife();
    public event LoseLife OnLoseLife;

    public delegate void MeatThrownEvent();
    public event MeatThrownEvent OnMeatThrownEvent;

    [SerializeField] public int _lifeCount;

    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private GameObject _currentMeat;
    private float _meatTimer;
    [SerializeField] private float _eatDuration;

    [SerializeField] private Camera mainCamera;

    public delegate void GameListen();
    public event GameListen doorClickedOn;
    public event GameListen birdClickedOn;
    public event GameListen puzzleClickedOn;

    public delegate void PigeonCallRequested();
    public event PigeonCallRequested OnPigeonCallRequested;
    private void Awake()
    {
         
    }

    public enum PlayerState
    {
        _idle, _walking
    }

    private PlayerState _state;


void Start()
    {
        _inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        UpdateAnim();
        
        if(_inventory != null)
        {
            _meatCount=_inventory.CountItem(itemType.Meat);
        }


        if (Input.GetKeyDown(KeyCode.Space) && _meatCount != 0)
        {
            MeatThrow();
            _inventory.RemoveItem(itemType.Meat);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            OnPigeonCallRequested?.Invoke();
            Debug.Log("Pigeon call requested");
        }

        ClickedSystem();
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

        if(vertical != 0 ||  horiztonal != 0)
        {
            ChangeState(PlayerState._walking);
        }
        else
        {
            ChangeState(PlayerState._idle);
        }
 
    }

    private void MeatThrow()
    {
        _meatSpawnPoint = transform.position + transform.forward * 1.5f + Vector3.up;
        _currentMeat= Instantiate(_meat,_meatSpawnPoint, Quaternion.identity);

        if (_currentMeat != null)
        {
            Rigidbody rb = _currentMeat.GetComponent<Rigidbody>();

            Vector3 throwDirection = transform.forward + Vector3.up * 0.3f;
            rb.AddForce(throwDirection.normalized * _meatThrowForce, ForceMode.Impulse);
            /*
            _meatTimer+= Time.deltaTime;

            if (_meatTimer >= _eatDuration)
            {
                GameObject.Destroy(_currentMeat);

            }
            */
        }

        OnMeatThrownEvent?.Invoke();
        Debug.Log("meat thrown");
    }

    public Vector3 GetMeatLocation()
    {
        return _currentMeat.transform.position;
    }

    private void UpdateAnim()
    {
        switch(_state)
        {
            case PlayerState._idle:
                playerAnim.SetBool("isWalking", false);
                break;
            case PlayerState._walking:
                playerAnim.SetBool("isWalking", true);
                break; 
        }
    }

    private void ChangeState(PlayerState newState)
    {
        _state = newState;
    }


    private void OnCollisionEnter(Collision other)
    {
       if (other.gameObject.CompareTag("Lion"))
        {
            Debug.Log("minus one life");
            _lifeCount--;
            transform.position = _spawnPoint.position;
            OnLoseLife?.Invoke();
           
        }
    }

    private void ClickedSystem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.GetComponent<Door>() != null)
                {
                    doorClickedOn?.Invoke();
                }
                if (hit.transform.gameObject.name == "Pigeon")
                {
                    birdClickedOn?.Invoke();
                }
                if (hit.transform.gameObject.GetComponent<PuzzleLogic>() != null)
                {
                    puzzleClickedOn?.Invoke();
                }
            }
        }
    }



}
