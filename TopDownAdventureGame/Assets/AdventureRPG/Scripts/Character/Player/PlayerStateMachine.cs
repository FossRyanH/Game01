using System;
using UnityEngine;

[Serializable]
public class PlayerStateMachine : Statemachine
{
    PlayerController _player;

    public PlayerStateMachine(PlayerController player)
    {
        _player = player;
    }
}

public enum StateType { Normal, Combat, Sneak }