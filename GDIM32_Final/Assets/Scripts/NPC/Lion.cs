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
    private float _stateTimer;
    private Transform _playerTransform;
    private bool _triggered;

    public delegate void LionTriggered();
    public event LionTriggered _lionTriggered;
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
    }


    private void EnterNewState(LionState state)
    {
        switch (state)
        {
            case LionState._idle:
                _stateTimer = 0.0f;
                _agent.isStopped = true;
                _animator.SetBool("Moving", false);
                _animator.SetTrigger("Calm");
                
                break;

            case LionState._wandering:
                _agent.isStopped = false;
                _agent.speed = 3.5f;
                Vector3 randomDirection = Random.insideUnitSphere * _wanderDistance;
                randomDirection.y = 0;

                Vector3 wanderDestination = transform.position + randomDirection;

                NavMeshHit hit;

                if (NavMesh.SamplePosition(wanderDestination, out hit, _wanderDistance, NavMesh.AllAreas))
                {
                    _agent.SetDestination(hit.position);
                }
                _animator.SetBool("Moving", true);
                _animator.SetTrigger("Calm");
                
                break;

            case LionState._pursuing:
                _agent.isStopped = false;
                _agent.speed = 5.0f;
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
        _agent.SetDestination(_playerTransform.position);
 
        
    }

    private void CheckDistance()
    {
        bool inRange;

        if (Vector3.Distance(transform.position, _playerTransform.position) <= _triggerDistance)
        {
            inRange = true;
        }
        else
        {

            inRange = false;

        }

        if (inRange && !_triggered)
        {
            _triggered = true;
            _lionTriggered?.Invoke();
            ChangeState(LionState._pursuing);
        }
        if (!inRange)
        {
            _triggered = false;
            ChangeState(LionState._wandering);
        }
    }

}
