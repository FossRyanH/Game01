using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] PlayerControlsSO playerControlsSO;

    #region Input Vars
    PlayerInputs _gameInputs;
    GameInputType _gameInputType;
    Dictionary<GameInputType, InputActionMap> _actionMaps;
    #endregion

    protected override void Init()
    {
        if (_actionMaps == null)
            _actionMaps = new Dictionary<GameInputType, InputActionMap>();
        
        if (_gameInputs == null)
        {
            _gameInputs = new PlayerInputs();
            _gameInputs.Player.Movement.performed += ctx => playerControlsSO.HandleMovement(ctx.ReadValue<Vector2>());
            _actionMaps.Add(GameInputType.Game, _gameInputs.Player);
        }
        _gameInputs.Enable();
    }
}
public enum GameInputType { Game, Menus, Dialogue }
