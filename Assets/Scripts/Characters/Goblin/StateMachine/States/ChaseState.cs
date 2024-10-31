using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public override void Do(){ controller.ChasePlayer(); }
    public override void Enter(){}
    public override void Exit(){}
    public override void SetController(StateController stateController){ controller = stateController; }
}
