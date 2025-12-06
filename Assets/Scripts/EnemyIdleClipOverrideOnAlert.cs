using System;
using UnityEngine;

public class EnemyIdleClipOverrideOnAlert : MonoBehaviour
{
    private const string IDLE_ANIMATION = "Idle";

    [SerializeField] private AnimationClip idleOverrideClip;

    private Animator animator;
    private AnimatorOverrideController runtimeOverride;
    private EnemyAnimator enemyAnimator;

    private void Awake()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        animator = GetComponent<Animator>();

        var originalOverride = animator.runtimeAnimatorController as AnimatorOverrideController;

        runtimeOverride = Instantiate(originalOverride);
        animator.runtimeAnimatorController = runtimeOverride;
    }

    private void Start()
    {
        enemyAnimator.AlertFinished += EnemyAnimatorAlertFinished;
    }

    private void EnemyAnimatorAlertFinished(object sender, EventArgs e)
    {
        runtimeOverride[IDLE_ANIMATION] = idleOverrideClip;
        enemyAnimator.AlertFinished -= EnemyAnimatorAlertFinished;
    }
}