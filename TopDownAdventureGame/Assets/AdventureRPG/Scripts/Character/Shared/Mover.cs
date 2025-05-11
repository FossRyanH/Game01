using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Mover : MonoBehaviour
{
    #region Movement Related
    public float MoveSpeed;
    public MoveType CurrentMoveType
    {
        get { return _moveType; }
    }
    MoveType _moveType;
    [SerializeField] float accelerationSpeed = 10f;
    [SerializeField] float rotationSpeed = 10f;
    #endregion

    #region Misc
    IMovementHandler _moveHandler;
    Vector3 _moveDir;
    public Vector2 InputDir { get; private set; }
    [SerializeField] float deadzone = 0.1f;
    #endregion

    #region Events
    public event EventHandler<MoveTypeChangedEventArgs> OnMoveTypeChanged;
    #endregion

    public void Initialize(IMovementHandler movementHandler)
    {
        _moveHandler = movementHandler;
    }

    /// <summary>
    /// converts the movement locally to global position
    /// </summary>
    /// <param name="worldDir"></param>
    public void SetMoveDir(Vector3 worldDir)
    {
        _moveDir = worldDir;
    }

    /// <summary>
    /// Fires an event based on the given params of how the character will be moving as well as how fast.
    /// </summary>
    /// <param name="newType"></param>
    public void SetMoveType(MoveType newType)
    {
        if (_moveType == newType) return;

        MoveType previous = _moveType;
        _moveType = newType;
        UpdateMoveSpeed(newType);

        OnMoveTypeChanged?.Invoke(this, new MoveTypeChangedEventArgs(previous, newType));
    }

    /// <summary>
    /// Takes in the input if any to manipulate the direction in which the character will move.
    /// </summary>
    /// <param name="inputDir"></param>
    public void SetInputDir(Vector2 inputDir)
    {
        InputDir = inputDir;
    }

    /// <summary>
    /// Updates the movement speed based upon whether or not the character is running, walking, idle...etc
    /// </summary>
    public void UpdateMoveSpeed(MoveType movetype)
    {
        _moveType = movetype;

        switch (movetype)
        {
            case MoveType.Walk:
                MoveSpeed = 2f;
                break;
            case MoveType.Run:
                MoveSpeed = 6f;
                break;
            case MoveType.Sprint:
                MoveSpeed = 9f;
                break;
            case MoveType.Sneak:
                MoveSpeed = 1f;
                break;
            case MoveType.Idle:
                MoveSpeed = 0f;
                break;
            default:
                Debug.Log("Move type not chosen.");
                break;
        }
    }

    /// <summary>
    /// The functionality to actually move any character this component is attached too.
    /// </summary>
    public void Move(Vector3 direction)
    {
        if (direction.sqrMagnitude < 0.01f) return;

        Vector3 setDir = new Vector3(direction.x, 0f, direction.y);
        transform.position += setDir * MoveSpeed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(setDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
public enum MoveType { Idle, Walk, Run, Sprint, Sneak }
