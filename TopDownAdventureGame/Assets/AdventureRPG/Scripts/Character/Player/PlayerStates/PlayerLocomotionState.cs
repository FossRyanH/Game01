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
        Mover.Move(Player.InputDir);
        SetMoveSpeed();
    }

    void SetMoveSpeed()
    {
        if (Player.InputDir.magnitude > 1f)
        {
            Mover.SetMoveType(MoveType.Sprint);
        }
        else if (Player.InputDir.magnitude <= 1f)
        {
            Mover.SetMoveType(MoveType.Run);
        }
        else if (Player.InputDir.magnitude <= 0.5f)
        {
            Mover.SetMoveType(MoveType.Walk);
        }
        else
        {
            Mover.SetMoveType(MoveType.Idle);
        }
    }
}