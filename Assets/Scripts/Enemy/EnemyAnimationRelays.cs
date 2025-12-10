using UnityEngine;

public class EnemyAnimationEventRelays : MonoBehaviour
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

    public void ProjectileLaunch()
    {
        enemyController.OnProjectileLaunch();
    }
}