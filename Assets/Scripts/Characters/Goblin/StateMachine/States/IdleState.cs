using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private float _currentTime;
    public override void Do()
    {
        if (_currentTime < 2)
        {
            _currentTime += Time.deltaTime;
        } else
        {
            controller.ChangeState(controller.walkState);
        }
    }

    public override void Enter(){_currentTime = 0;}

    public override void Exit(){}

    public override void SetController(StateController stateController){ controller = stateController; }
}
