using System;
using UnityEngine;

public class ClosingDoor : MonoBehaviour
{
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
}