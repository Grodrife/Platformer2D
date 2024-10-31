using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State
{
    private float _currentTime;
    public override void Do()
    {
        if (_currentTime < 2)
        {
            _currentTime += Time.deltaTime;
            controller.enemyController.MoveEnemy();
        } else
        {
            controller.ChangeState(controller.idleState);
        }
        
    }
    public override void Enter(){ _currentTime = 0; }
    public override void Exit(){}
    public override void SetController(StateController stateController){ controller = stateController; }
}
