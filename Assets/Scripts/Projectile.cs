using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    private void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.left, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            player.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}