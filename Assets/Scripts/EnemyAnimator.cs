using System;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
    private static readonly int PlayerDetected = Animator.StringToHash("PlayerDetected");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Died = Animator.StringToHash("Died");
    private static readonly int Alert = Animator.StringToHash("Alert");
    private Animator animator;
    private EnemyController enemyController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyController = GetComponentInParent<EnemyController>();
    }

    private void Start()
    {
        enemyController.IsAttackingChanged += EnemyControllerOnIsAttackingChanged;
        enemyController.PlayerDetectedChanged += EnemyControllerOnPlayerDetectedChanged;
        enemyController.TookDamage += EnemyControllerOnTookDamage;
        enemyController.Died += EnemyControllerOnDied;
        enemyController.Alerted += EnemyControllerOnAlerted;
    }

    private void EnemyControllerOnAlerted(object sender, EventArgs e)
    {
        animator.SetTrigger(Alert);
        var alertLength = animator.GetCurrentAnimatorStateInfo(0).length;
        Invoke(nameof(FinishAlert), alertLength);
    }

    public void FinishAlert()
    {
        enemyController.StartFollowing();
    }

    private void EnemyControllerOnDied(object sender, EventArgs e)
    {
        animator.SetTrigger(Died);
    }

    private void EnemyControllerOnTookDamage(object sender, EventArgs e)
    {
        animator.SetTrigger(Hit);
    }

    private void EnemyControllerOnPlayerDetectedChanged(object sender, bool e)
    {
        animator.SetBool(PlayerDetected, e);
    }

    private void EnemyControllerOnIsAttackingChanged(object sender, bool e)
    {
        animator.SetBool(IsAttacking, e);
    }
}