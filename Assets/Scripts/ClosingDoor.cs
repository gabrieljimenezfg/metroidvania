using System;
using UnityEngine;

public class ClosingDoor : MonoBehaviour
{
    [SerializeField] BossLevelManager bossLevelManager;
    private string CLOSED = "Closed";
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Close()
    {
        animator.SetBool(CLOSED, true);
    }

    public void StartFighting()
    {
        bossLevelManager.StartFight();
    }
}