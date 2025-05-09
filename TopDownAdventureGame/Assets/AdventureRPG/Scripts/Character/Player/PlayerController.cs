using UnityEngine;
using System;
using UnityEditor.Rendering;

public class PlayerController : MonoBehaviour
{
    #region Inputs
    [field: SerializeField] public PlayerControlsSO PlayerControls { get; private set; }
    public Vector2 InputDir;
    #endregion

    #region Components
    [field: SerializeField] public BodySwapper CharacterRig { get; private set; }
    [field: SerializeField] public PlayerStateMachine PlayerStateMachine { get; private set; }
    public Mover Mover { get; private set; }
    [field: SerializeField] public AnimationController AnimationController { get; private set; }
    #endregion

    #region Model Vars
    [SerializeField] BodyType bodyType = 0;
    #endregion

    void Awake()
    {
        CharacterRig = GetComponentInChildren<BodySwapper>();
        PlayerStateMachine = new PlayerStateMachine(this);
        Mover = GetComponent<Mover>();
        AnimationController = GetComponent<AnimationController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetBodyType();
        PlayerStateMachine.InitializeState(new PlayerLocomotionState(this));
    }

    void Update()
    {
        PlayerStateMachine.Execute();
    }

    void FixedUpdate()
    {
        PlayerStateMachine.ExecutePhysics();
    }

    void SetBodyType()
    {
        CharacterRig.InstantiateBody(bodyType);
    }
}
