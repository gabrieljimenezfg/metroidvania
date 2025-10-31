using Unity.VisualScripting;
using UnityEngine;

public class EscapatrajoController : EnemyController
{
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");

    private new void Awake()
    {
        base.Awake();
    }
    private new void Update()
    {
        base.Update();
        // if (isAttacking)
        // {
        // }
    }

    public void FinishAttack()
    {
        var isInStopRange = CheckIfIsInStopDistanceRange();
        if (!isInStopRange)
        {
            SetIsAttacking(false);
        }
    }
}