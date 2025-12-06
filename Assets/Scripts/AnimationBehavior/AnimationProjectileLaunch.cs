using System;
using UnityEngine;

public class AnimationProjectileLaunch : MonoBehaviour
{
    private EnemyController enemyController;

    private void Awake()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }


    public void ProjectileLaunch()
    {
        enemyController.OnProjectileLaunch();
    }
}