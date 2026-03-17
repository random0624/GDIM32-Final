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
    
    private Vector3 _meatLocation;
    private bool _canSeeMeat;
    
    [SerializeField] private float _meatReachDistance = 1.0f;
    [SerializeField] private float _meatNoticeDistance = 10f;

    private bool _isEatingMeat;
    private float _meatPauseTimer;

    [SerializeField] private float _meatPauseDuration = 4f;

    private Vector3 _stuckStartPosition;
    private float _stuckTimer;
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
        _stuckStartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (_isEatingMeat)
        {
            _meatPauseTimer -= Time.deltaTime;
            _agent.isStopped = true;
            _animator.Play("idle");

            if (_meatPauseTimer <= 0f)
            {
                _isEatingMeat = false;
                ChangeState(LionState._idle);
            }

            return;
        }
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

        if (_state != LionState._idle)
        {
            CheckStuck();
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
        /*
        _animator.Play("run");


       

        if (_canSeeMeat)
        {
            _lionTarget=_meatLocation;
        }

        else
        {
            _lionTarget = _playerTransform.position;
        }
        _agent.SetDestination(_lionTarget);

      //  _canSeeMeat = false;
        if (_canSeeMeat &&Vector3.Distance(transform.position, _meatLocation) <= _meatReachDistance)
        {
            Debug.Log("meat reached");
            _canSeeMeat = false;
            ChangeState(LionState._idle);
        }
        */

        //redo

        /* safe ver
        _animator.Play("run");

        if (_canSeeMeat)
        {
            _lionTarget = _meatLocation;
            _agent.isStopped = false;
            _agent.SetDestination(_lionTarget);

            float meatDistance = Vector3.Distance(transform.position, _meatLocation);

            Debug.Log("Going to meat: " + _meatLocation +
                      " | CurrentPos: " + transform.position +
                      " | MeatDist: " + meatDistance);

            if (!_agent.pathPending && meatDistance <= _meatReachDistance)
            {
                Debug.Log("Reached meat");
                _canSeeMeat = false;
                _agent.ResetPath();
                ChangeState(LionState._idle);
            }

            return;
        }

        _lionTarget = _playerTransform.position;
        _agent.isStopped = false;
        _agent.SetDestination(_lionTarget);
        */

        _animator.Play("run");
        _agent.isStopped = false;

        // Meat mode: no line of sight, only go to stored meat location
        if (_canSeeMeat)
        {
            _lionTarget = _meatLocation;
            _agent.SetDestination(_lionTarget);

            if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance + 0.1f)
            {
                _canSeeMeat = false;
                _isEatingMeat = true;
                _meatPauseTimer = _meatPauseDuration;

                _agent.ResetPath();
                _agent.isStopped = true;
                _animator.Play("idle");
            }

            return;
        }

        // Normal player pursuit
        _lionTarget = _playerTransform.position;
        _agent.SetDestination(_lionTarget);
    }

    private void CheckDistance()
    {

        /*
        if (Vector3.Distance(transform.position, _playerTransform.position) <= _triggerDistance)
        {
            _playerInRange = true;
        }
        else
        {

            _playerInRange = false;

        }

        if (_playerInRange && !_triggered && CanSeePlayer() && !GameController.Instance.Player.IsHidden) //IsHidden check if the player is hidden in a bush
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
        */

        //had to redo (safe ver)
        
        if (_canSeeMeat)
        {
            return;
        }

        if (_playerTransform == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);
        _playerInRange = distanceToPlayer <= _triggerDistance;

        bool canSeePlayer = CanSeePlayer();
        bool playerIsHidden = GameController.Instance.Player.IsHidden;

        if (_playerInRange && canSeePlayer && !playerIsHidden)
        {
            if (!_triggered)
            {
                _triggered = true;
                _lionTriggered?.Invoke();
            }

            if (_state != LionState._pursuing)
            {
                ChangeState(LionState._pursuing);
            }

            return;
        }

        _triggered = false;

        if (_state == LionState._pursuing)
        {
            ChangeState(LionState._wandering);
            return;
        }

        CheckNewDestinationNeeded();

        if (_lionNeedsNewDestination && _state != LionState._wandering)
        {
            ChangeState(LionState._wandering);
        }
        //   Debug.Log("Distance: " + distanceToPlayer + " | InRange: " + _playerInRange + " | CanSeePlayer: " + canSeePlayer + " | State: " + _state + " | CanSeeMeat: " + _canSeeMeat);
        
      

    }

    private bool CanSeePlayer()

    {
        /*
        if(_playerTransform == null || _eyepoint == null)
        {
            return false;
        }

//Added logic to check if the player is hidden in a bush
        if (GameController.Instance.Player.IsHidden)
            return false;

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
        return Physics.Raycast(_eyepoint.position, toPlayer.normalized, distance, _playerMask);*/

        /*
        if (_playerTransform == null || _eyepoint == null)
        {
            return false;
        }

        if (GameController.Instance.Player.IsHidden)
        {
            return false;
        }

        Vector3 toPlayer = _playerTransform.position - _eyepoint.position;
        float distance = toPlayer.magnitude;

        if (distance > _viewDistance)
        {
            return false;
        }

        float angle = Vector3.Angle(_eyepoint.forward, toPlayer);

        if (angle > _viewAngle * 0.5f)
        {
            return false;
        }

        if (Physics.Raycast(_eyepoint.position, toPlayer.normalized, distance, _obstacleMask))
        {
            return false;
        }

        return true;
        */

        //had to redo
        if (_playerTransform == null || _eyepoint == null)
        {
            return false;
        }

        if (GameController.Instance.Player.IsHidden)
        {
            return false;
        }

        Vector3 toPlayer = _playerTransform.position - _eyepoint.position;
        float distance = toPlayer.magnitude;

        if (distance > _viewDistance)
        {
            return false;
        }

        float angle = Vector3.Angle(_eyepoint.forward, toPlayer);
        if (angle > _viewAngle * 0.5f)
        {
            return false;
        }

        RaycastHit hit;
        if (Physics.Raycast(_eyepoint.position, toPlayer.normalized, out hit, distance))
        {
            Debug.Log("Ray hit: " + hit.collider.name);

            if (hit.collider.transform == _playerTransform || hit.collider.transform.IsChildOf(_playerTransform))
            {
                return true;
            }

            if (((1 << hit.collider.gameObject.layer) & _obstacleMask) != 0)
            {
                return false;
            }
        }

        return false;
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

    private void CheckStuck()
    {
        

       

        _stuckTimer += Time.deltaTime;

        if (_stuckTimer >= 0.5f)
        {
            float movedDistance = Vector3.Distance(_stuckStartPosition, transform.position);

            if (movedDistance <= 0.5f && !_agent.pathPending)
            {
                RecoverFromStuck();
            }

            _stuckStartPosition = transform.position;
            _stuckTimer = 0f;
        }
        /*

        if( _stuckTimer >= 0.5f)
        {
            Vector3 newPosition= transform.position;


            if(Vector3.Distance(_stuckStartPosition, newPosition) <= 2)
            {
                // _lionNeedsNewDestination = true;
                Debug.Log("lion is stuck");
                ChangeState(LionState._wandering);
            }
        }

        _stuckTimer = 0;
        _stuckStartPosition = transform.position;
        */
    }

    private void RecoverFromStuck()
    {
        Debug.Log("lion is stuck");

        _agent.ResetPath();

        for (int i = 0; i < 8; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * _wanderDistance;
            randomDirection.y = 0f;

            Vector3 candidate = transform.position + randomDirection;

            if (NavMesh.SamplePosition(candidate, out NavMeshHit hit, _wanderDistance, NavMesh.AllAreas))
            {
                NavMeshPath path = new NavMeshPath();

                if (_agent.CalculatePath(hit.position, path) && path.status == NavMeshPathStatus.PathComplete)
                {
                    _currentLionWanderingPosition = hit.position;
                    _agent.SetDestination(hit.position);
                    _state = LionState._wandering;
                    return;
                }
            }
        }
    }

  
 

    private void ReactToMeatThrow()
    {
        /*safe ver
        Vector3 thrownMeatLocation = GameController.Instance.Player.GetMeatLocation();

        float distanceToMeat = Vector3.Distance(transform.position, thrownMeatLocation);
        Debug.Log("Distance to thrown meat: " + distanceToMeat);

        if (distanceToMeat <= _meatNoticeDistance)
        {
            if (NavMesh.SamplePosition(thrownMeatLocation, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                _meatLocation = hit.position;
                _canSeeMeat = true;

                Debug.Log("Lion noticed meat at: " + _meatLocation);

                ChangeState(LionState._pursuing);
            }
            else
            {
                Debug.Log("No valid navmesh point near meat");
            }
        }
        else
        {
            Debug.Log("Meat outside notice distance");
        }
        */
        Vector3 thrownMeatLocation = GameController.Instance.Player.GetMeatLocation();

        float distanceToMeat = Vector3.Distance(transform.position, thrownMeatLocation);
        Debug.Log("Distance to thrown meat: " + distanceToMeat);


        if (distanceToMeat > _meatNoticeDistance)
        {
            Debug.Log("Meat outside notice distance");
            return;
        }

        if (NavMesh.SamplePosition(thrownMeatLocation, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            _meatLocation = hit.position;
            _canSeeMeat = true;

            _agent.ResetPath();
            _agent.isStopped = false;
            _agent.SetDestination(_meatLocation);

            Debug.Log("Lion noticed meat at: " + _meatLocation);

            ChangeState(LionState._pursuing);
        }
        else
        {
            Debug.Log("No valid navmesh point near meat");
        }
    }

//When player is hidden in a bush, lion stops pursuing and wanders
    private void OnPlayerHidden()
    {
        if (_canSeeMeat) return;
        if (_state == LionState._pursuing)
            ChangeState(LionState._wandering);
        
    }

//State is NOT changed when player is revealed
    private void OnPlayerRevealed() { }


    private void OnPlayerWrongKey()
    {
        if (_canSeeMeat) return;
        
        
        ChangeState(LionState._pursuing);
    }

    private void OnPlayerWrongAnswer()
    {
        if (_canSeeMeat) return;
        
        
        ChangeState(LionState._pursuing);
    }
    private void OnEnable()
    {
        GameController.Instance.Player.OnMeatThrownEvent += ReactToMeatThrow;
        GameController.Instance.Player.OnPlayerHidden += OnPlayerHidden;
        GameController.Instance.Player.OnPlayerRevealed += OnPlayerRevealed;
        GameController.Instance.CurrentDoor.wrongKey += OnPlayerWrongKey;
        GameController.Instance.CurrentPuzzle.incorrectAnswer += OnPlayerWrongAnswer;



    }

    private void OnDisable()
    {
        GameController.Instance.Player.OnMeatThrownEvent -= ReactToMeatThrow;
        GameController.Instance.Player.OnPlayerHidden -= OnPlayerHidden;
        GameController.Instance.Player.OnPlayerRevealed -= OnPlayerRevealed;
        GameController.Instance.CurrentDoor.wrongKey -= OnPlayerWrongKey;
        GameController.Instance.CurrentPuzzle.incorrectAnswer -= OnPlayerWrongAnswer;
    }
}
