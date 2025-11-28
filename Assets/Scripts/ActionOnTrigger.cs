using System;
using UnityEngine;
using UnityEngine.Events;

public class ActionOnTrigger : MonoBehaviour
{
    public event EventHandler OnPlayerEnter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            OnPlayerEnter?.Invoke(this, EventArgs.Empty);
        }
    }
}