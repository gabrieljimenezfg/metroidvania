using UnityEngine;

public class GoblinController : EnemyController
{
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");


    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform spearSpawnPoint;

    private new void Awake()
    {
        base.Awake();
    }

    public override void OnProjectileLaunch()
    {
        Instantiate(projectilePrefab, spearSpawnPoint.position, spearSpawnPoint.rotation);
    }
}