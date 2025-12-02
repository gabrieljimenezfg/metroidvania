using UnityEngine;

public class PeterController : EnemyController
{
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");


    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform spearSpawnPoint;

    private new void Awake()
    {
        base.Awake();
    }

    private new void Update()
    {
        base.Update();
        if (isAttacking)
        {
            var isInStopRange = CheckIfIsInStopDistanceRange();
            if (!isInStopRange)
            {
                SetIsAttacking(false);
            }
        }
    }

    public override void OnProjectileLaunch()
    {
        Instantiate(projectilePrefab, spearSpawnPoint.position, spearSpawnPoint.rotation);
    }
}