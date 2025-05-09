using System.Collections;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Mover mover;
    [SerializeField] Animator animator;

    void Awake()
    {
        StartCoroutine(FindAnimatorDelay());
        mover = GetComponent<Mover>();
    }

    void OnEnable()
    {
        mover.OnMoveTypeChanged += HandleMoveTypeChanged;
    }

    void OnDisable()
    {
        mover.OnMoveTypeChanged -= HandleMoveTypeChanged;
    }

    void Update()
    {
        float target = mover.InputDir.magnitude;
        float current = animator.GetFloat("MovementInput");
        float smoothed = Mathf.Lerp(current, target, Time.deltaTime * 10f);
        animator.SetFloat("MovementInput", smoothed);
    }

    void HandleMoveTypeChanged(object sender, MoveTypeChangedEventArgs e)
    {
        if (e.NewType == MoveType.Sprint)
        {
            animator.SetFloat("MovementInput", 2f);
        }
        else if (e.NewType == MoveType.Walk)
        {
            animator.SetFloat("MovementInput", 0.5f);
        }
        else
        {
            animator.SetFloat("MovementInput", 1f);
        }
    }

    IEnumerator FindAnimatorDelay()
    {
        yield return new WaitForFixedUpdate();
        animator = GetComponentInChildren<Animator>();
        StopCoroutine(FindAnimatorDelay());
    }
}
