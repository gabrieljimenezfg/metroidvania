using System;
using UnityEngine;

public class AnimationProjectileLaunch : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;

    public void ProjectileLaunch()
    {
        enemyController.OnProjectileLaunch();
    }
}