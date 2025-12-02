using System;
using UnityEngine;

public class AnimationAttackFinish : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;

    public void FinishAttack()
    {
        enemyController.FinishAttack();
    }
}