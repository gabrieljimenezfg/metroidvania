using System;
using UnityEngine;

public class FallRespawn : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
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
        PlayerController.Instance.transform.position = respawnPoint.position;
    }
}