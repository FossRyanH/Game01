using UnityEngine;
using System;

public class PlayerLocomotionState : PlayerBaseState
{
    public PlayerLocomotionState(PlayerController player) : base(player) {}

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();
        Mover.Move();

        if (Player.InputDir.magnitude <= 0)
        {
            Mover.SetMoveType(MoveType.Idle);
        }
        else if (Player.InputDir.magnitude >= 0.5f)
        {
            Mover.SetMoveType(MoveType.Run);
        }
        else
        {
            Mover.SetMoveType(MoveType.Walk);
        }
    }
}