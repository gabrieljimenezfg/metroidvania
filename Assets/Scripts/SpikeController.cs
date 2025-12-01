using UnityEngine;

public class SpikeController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField]
    private float maximumFlyTime;
    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(-transform.right * speed);
        Invoke(nameof(DestroySelf), maximumFlyTime);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            speed = 0;
        }
        Destroy(this.gameObject);
    }
}