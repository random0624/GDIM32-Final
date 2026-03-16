using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Lion : MonoBehaviour
{

    
    [SerializeField] private float _idleDuration;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _wanderDistance;
    [SerializeField] private float _triggerDistance;
    [SerializeField] private Animator _animator;

    [SerializeField] private Transform _eyepoint;
    [SerializeField] private float _viewDistance;
    [SerializeField] private float _viewAngle;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private LayerMask _playerMask;

    
    private float _stateTimer;
    private Transform _playerTransform;
    private bool _triggered;

    private bool _playerInRange;

    private bool _lionNeedsNewDestination; //more like can have new destination
    private Vector3 _currentLionWanderingPosition;

    public delegate void LionTriggered();
    public event LionTriggered _lionTriggered;

    private Vector3 _lionTarget;
    
    

    public enum LionState
    {
        _idle, _wandering, _pursuing
    }


    private LionState _state;
    void Start()
    {
        ChangeState(LionState._idle);
        _playerTransform =  GameController.Instance.Player.transform;
        
        _triggered = false;
        _lionNeedsNewDestination = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case LionState._idle: Idle(); break;
            case LionState._wandering: Wandering(); break;
            case LionState._pursuing: Pursuing(); break;
        }

        CheckDistance();

        if(_playerTransform != null && _eyepoint != null)
        {
            Vector3 direction = (_playerTransform.position - _eyepoint.position).normalized;
            Debug.DrawRay(_eyepoint.position, direction, CanSeePlayer()? Color.blue:Color.red); 
        }

        //debugging

        /*

        if (CanSeePlayer())
        {
            Debug.Log("can see");
        }
        */
    }


    private void EnterNewState(LionState state)
    {
        switch (state)
        {
            case LionState._idle:
                _lionNeedsNewDestination = true;
                _stateTimer = 0.0f;
                _agent.isStopped = true;
                _animator.SetBool("Moving", false);
                _animator.SetTrigger("Calm");
                
                break;

            case LionState._wandering:
                _lionNeedsNewDestination = false;
                _agent.isStopped = false;
                _agent.speed = 3.5f;
                Vector3 randomDirection = Random.insideUnitSphere * _wanderDistance;
                randomDirection.y = 0;

                Vector3 wanderDestination = transform.position + randomDirection;

                NavMeshHit hit;

                if (NavMesh.SamplePosition(wanderDestination, out hit, _wanderDistance, NavMesh.AllAreas))
                {
                    Debug.Log("found new destination");
                    _agent.SetDestination(hit.position);
                    _currentLionWanderingPosition = hit.position;
                }
                //add code here that doesn't let it move out of wandering unless destination met OR navmesh is given a new destination
                _animator.SetBool("Moving", true);
                _animator.SetTrigger("Calm");
                
                break;

            case LionState._pursuing:
                _lionNeedsNewDestination = true;
                _agent.isStopped = false;
                _agent.speed = 6.0f;
                _animator.SetBool("Moving", true);
                _animator.SetTrigger("Triggered");
                _animator.Play("roar");
                break;
        }
    }

    private void ChangeState(LionState newState)
    {
        _state= newState;
        EnterNewState(_state);
    }

    public void Idle()
    {
        //have them chill for a set amt of time
        _stateTimer += Time.deltaTime;
        _animator.Play("idle");

        if (_stateTimer>= _idleDuration)
        {
            ChangeState(LionState._wandering);
        }
    }

    public void Wandering()
    {
        
        _animator.Play("walk");

        if (!_agent.pathPending&& _agent.hasPath &&_agent.remainingDistance<= _agent.stoppingDistance)
        {
            ChangeState(LionState._idle);
        }
       
    }

    public void Pursuing()
    {
        _animator.Play("run");
        _lionTarget = _playerTransform.position;
        _agent.SetDestination(_lionTarget);
 
        
    }

    private void CheckDistance()
    {
        

        if (Vector3.Distance(transform.position, _playerTransform.position) <= _triggerDistance)
        {
            _playerInRange = true;
        }
        else
        {

            _playerInRange = false;

        }

        if (_playerInRange && !_triggered &&CanSeePlayer())
        {
            _triggered = true;
            _lionTriggered?.Invoke();
            ChangeState(LionState._pursuing);
        }
      
        if (!_playerInRange &&!CanSeePlayer())
        {
            _triggered = false;
            //need to move it back to a state when not in range
            //comment this next line out
           CheckNewDestinationNeeded();
            if (_lionNeedsNewDestination)
            {
                ChangeState(LionState._wandering);
            }
           //maybe try moving the entering wandering state somewhere else
        }
    }

    private bool CanSeePlayer()
    {
        if(_playerTransform == null || _eyepoint == null)
        {
            return false;
        }

        Vector3 toPlayer = _playerTransform.position- _eyepoint.position;

        if (toPlayer.sqrMagnitude> _viewDistance*_viewDistance)
        {
            return false;
        }

        float angle = Vector3.Angle(_eyepoint.forward, toPlayer);

        if(angle > _viewAngle * 0.5f)
        {
            return false;
        }

        float distance = toPlayer.magnitude;

        if(Physics.Raycast(_eyepoint.position, toPlayer, distance, _obstacleMask))
        {
            return false;
        }

        //first line im deleting if ts doesnt work
        return Physics.Raycast(_eyepoint.position, toPlayer.normalized, distance, _playerMask);
    }

    private void CheckNewDestinationNeeded()
    {
        float distance;
        distance = Vector3.Distance(_currentLionWanderingPosition, transform.position);
        if (distance <= 1)
        {
            _lionNeedsNewDestination = true;
            Debug.Log("reached destination");
        }

    }


    private void ReactToMeatThrow()
    {
        //if close enough
        //change navmesh dest to meat
        //change state to pursuing
        //might have to change the state machine a little so it can be set to any objects destination 
    }


    private void CheckMeatDistance()
    {
        //use triggerdistance
    }


  
}
