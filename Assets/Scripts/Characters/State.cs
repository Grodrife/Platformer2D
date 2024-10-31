using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected StateController controller;
    public abstract void Enter();
    public abstract void Do();
    public abstract void Exit();
    public abstract void SetController(StateController stateController);
}
