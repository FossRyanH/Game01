using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    #region Movement Related
    public float MoveSpeed;
    MoveType _moveType;
    [SerializeField] float rotationSpeed = 10f;
    #endregion

    #region Misc
    IMovementHandler _moveHandler;
    Vector3 _moveDir;
    public Vector2 InputDir { get; private set; }
    #endregion

    #region Events
    public event EventHandler<MoveTypeChangedEventArgs> OnMoveTypeChanged;
    #endregion

    public void Initialize(IMovementHandler movementHandler)
    {
        _moveHandler = movementHandler;
    }

    public void SetMoveDir(Vector3 worldDir)
    {
        _moveDir = worldDir;
    }

    public void SetMoveType(MoveType newType)
    {
        if (_moveType == newType) return;

        MoveType previous = _moveType;
        _moveType = newType;
        UpdateMoveSpeed();

        OnMoveTypeChanged?.Invoke(this, new MoveTypeChangedEventArgs(previous, newType));
    }

    public void SetInputDir(Vector2 inputDir)
    {
        InputDir = inputDir;
    }

    public void UpdateMoveSpeed()
    {
        switch (_moveType)
        {
            case MoveType.Walk:
                MoveSpeed = 2f;
                break;
            case MoveType.Run:
                MoveSpeed = 4f;
                break;
            case MoveType.Sprint:
                MoveSpeed = 7f;
                break;
            case MoveType.Sneak:
                MoveSpeed = 1.5f;
                break;
            default:
                MoveSpeed = 0f;
                break;
        }
    }

    public void Move()
    {
        if (InputDir.magnitude > 0.01f)
        {
            Vector3 direction = new Vector3(InputDir.x, 0f, InputDir.y);
            transform.position += direction * MoveSpeed * Time.fixedDeltaTime;
        }
    }
}
public enum MoveType { Walk, Run, Sprint, Sneak }
