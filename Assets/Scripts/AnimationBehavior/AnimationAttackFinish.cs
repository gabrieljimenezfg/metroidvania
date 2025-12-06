using System;
using UnityEngine;

public class AnimationAttackFinish : MonoBehaviour
{
    private EnemyController enemyController;

    private void Awake()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }

    public void FinishAttack()
    {
        enemyController.FinishAttack();
    }
}