using System;
using UnityEngine;

public class FightStartPoint : MonoBehaviour
{
    [SerializeField] BossLevelManager bossLevelManager;
    [SerializeField] ClosingDoor closingDoor;
    private ActionOnTrigger trigger;

    private void Awake()
    {
        trigger = GetComponentInChildren<ActionOnTrigger>();
    }

    private void Start()
    {
        trigger.OnPlayerEnter += OnPlayerEnter;        
    }

    private void OnPlayerEnter(object sender, EventArgs e)
    {
        closingDoor.Close();
    }
}
