using System;
using UnityEngine;

public interface IState
{
    void Enter();
    void Execute();
    void PhysicsUpdate();
    void Exit();
}