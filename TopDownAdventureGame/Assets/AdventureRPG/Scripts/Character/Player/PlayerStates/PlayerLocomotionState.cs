using UnityEngine;
using System;

public class PlayerLocomotionState : PlayerBaseState
{
    public PlayerLocomotionState(PlayerController player) : base(player) {}

    public override void Enter()
    {
        base.Enter();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Player.Mover.Move();
    }
}