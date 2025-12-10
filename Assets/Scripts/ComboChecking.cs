using System;
using UnityEngine;

public class ComboChecking : MonoBehaviour
{
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    public void StartAttack()
    {
        playerController.InvokeSwordAttacked();
    }

    public void FinishAttack1()
    {
        playerController.CheckCombo1();
    }

    public void FinishAttack2()
    {
        playerController.CheckCombo2();
    }

    public void FinishHeavyAttack()
    {
        playerController.FinishHeavyAttack();
    }
}