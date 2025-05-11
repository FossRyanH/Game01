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
        float inputMagnitude = mover.InputDir.magnitude;
        animator.SetFloat("MovementInput", inputMagnitude, 0.2f, Time.deltaTime);
    }

    /// <summary>
    /// Using the enum "MoveType" this will control the animation for movement, which will include all types of locomotion later on.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    /// There was a bug with finding the animator on a not-quite-yet-spawned character body, this fixes that bug by creating a delay before looking for it.
    /// </summary>
    IEnumerator FindAnimatorDelay()
    {
        yield return new WaitForFixedUpdate();
        animator = GetComponentInChildren<Animator>();
        StopCoroutine(FindAnimatorDelay());
    }
}
