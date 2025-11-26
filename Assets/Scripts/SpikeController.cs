using UnityEngine;

public class SpikeController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(-transform.right * speed);
    }
    
    void Update()
    {
        // Vector3 direction = rb.linearVelocity.normalized;
        // Vector2 objetive = transform.position + direction;
        // transform.LookAt(objetive);
        // transform.Rotate(0, 90, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.gameObject.tag == "Player")
        // {
        //     Debug.Log("Launch");
        //     collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        //     speed = 0;
        // }
        // Destroy(this.gameObject);
    }
}