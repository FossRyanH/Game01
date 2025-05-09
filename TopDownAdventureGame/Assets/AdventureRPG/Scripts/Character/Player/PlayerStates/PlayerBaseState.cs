using System;
using UnityEngine;

public class PlayerBaseState : IState, IPlayerInputListener
{
    protected PlayerController Player;
    protected Mover Mover;

    public PlayerBaseState(PlayerController player) 
    {
        Player = player;
        Mover = player.Mover;
    }

    public virtual void Enter() 
    {
        RegisterListeners();
    }

    public virtual void Execute() {}

    public  virtual void Exit() 
    {
        DeregisterListeners();
    }

    public virtual void PhysicsUpdate() {}

    void RegisterListeners()
    {
        Player.PlayerControls.Movement += Move;
        Mover.OnMoveTypeChanged += OnMoveTypeChanged;
    }

    void DeregisterListeners()
    {
        Player.PlayerControls.Movement -= Move;
        Mover.OnMoveTypeChanged -= OnMoveTypeChanged;
    }

    public void Move(Vector2 inputDir)
    {
        Player.InputDir = inputDir.normalized;
        Mover.SetInputDir(Player.InputDir);
    }

    protected virtual void OnMoveTypeChanged(object sender, MoveTypeChangedEventArgs e) {}
}