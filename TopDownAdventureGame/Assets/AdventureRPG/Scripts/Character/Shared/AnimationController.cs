using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Mover mover;
    Animator _animator;

    // Player if applicaable
    PlayerController _player;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        mover = GetComponent<Mover>();
        
    }

    void OnEnable()
    {
        mover.OnMoveTypeChanged += HandleMoveTypeChanged;
    }

    void Update()
    {
        if (mover || _animator == null) return;

        float target = mover.InputDir.magnitude;
        float current = _animator.GetFloat("Movement");
        float smoothed = Mathf.Lerp(current, target, Time.deltaTime * 10f);
        _animator.SetFloat("Movement", smoothed);
    }

    void HandleMoveTypeChanged(object sender, MoveTypeChangedEventArgs e)
    {
        if (e.NewType == MoveType.Sprint)
        {
            _animator.SetFloat("Movement", 2f);
        }
        else if (e.NewType == MoveType.Walk)
        {
            _animator.SetFloat("Movement", 0.5f);
        }
        else
        {
            _animator.SetFloat("Movement", 1f);
        }
    }
}
