using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion : MonoBehaviour
{
    public enum LionState
    {
        _idle, _wandering, _pursuing
    }


    private LionState _state;
    void Start()
    {
        ChangeState(LionState._idle);
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
    }


    private void EnterNewState(LionState state)
    {
        switch (state)
        {
            case LionState._idle:
                //slkjflskdj
                break;

            case LionState._wandering:
                //sdajkfaj
                break;

            case LionState._pursuing:
                //akjhfdsfjh
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
        ChangeState(LionState._idle);
    }

    public void Wandering()
    {
        //find random destination and set navmesh destination to it
        ChangeState(LionState._wandering);
    }

    public void Pursuing()
    {
        //set navmesh destination to player
        ChangeState(LionState._pursuing);
    }

 
}
