using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerControlsSO", menuName = "Scriptable Objects/PlayerControls")]
public class PlayerControlsSO : ScriptableObject
{
    public event Action<Vector2> Movement;

    public void HandleMovement(Vector2 movement) => Movement?.Invoke(movement);
}
