using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.left, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // player hit
        }
        Destroy(gameObject);
    }
}
