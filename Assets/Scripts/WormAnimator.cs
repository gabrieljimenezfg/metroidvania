using System;
using UnityEngine;

public class WormAnimator : MonoBehaviour
{
    private const string IDLE_ANIMATION = "Idle";

    [SerializeField] private AnimationClip idleOverClip;

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
        runtimeOverride[IDLE_ANIMATION] = idleOverClip;
        enemyAnimator.AlertFinished -= EnemyAnimatorAlertFinished;
    }
}