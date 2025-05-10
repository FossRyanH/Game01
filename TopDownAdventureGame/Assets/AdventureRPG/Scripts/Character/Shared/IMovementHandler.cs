using System;
using UnityEngine;

public interface IMovementHandler
{
    void Move(Vector3 direction, float moveSpeed);
    void RotateToward(Vector3 direction, float rotationSpeed);
    public bool IsGrounded { get; }
    RaycastHit GroundHit { get; }
}