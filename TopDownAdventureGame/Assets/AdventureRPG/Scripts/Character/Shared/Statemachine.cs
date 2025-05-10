using System;
using UnityEngine;

[Serializable]
public class Statemachine
{
    [field: SerializeField] public IState CurrentState { get; private set; }

    public void InitializeState(IState firstState)
    {
        CurrentState = firstState;
        firstState?.Enter();
    }

    public void ChangeState(IState newState)
    {
        if (CurrentState == null) return;
        CurrentState?.Exit();
        CurrentState = newState;
        newState?.Enter();
    }

    public void Execute()
    {
        if (CurrentState != null)
        {
            CurrentState?.Execute();
        }
    }

    public void ExecutePhysics()
    {
        if (CurrentState != null)
        {
            CurrentState?.PhysicsUpdate();
        }
    }
}
