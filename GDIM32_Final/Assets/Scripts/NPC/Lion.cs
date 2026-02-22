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
        switch (_state)
        {
            case LionState._idle: Idle(); break;
            case LionState._wandering: Wandering(); break;
            case LionState._pursuing: Pursuing(); break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState()
    {

    }

    public void Idle()
    {
        //have them chill for a set amt of time
    }

    public void Wandering()
    {
        //find random destination and set navmesh destination to it
    }

    public void Pursuing()
    {
        //set navmesh destination to player
    }
}
