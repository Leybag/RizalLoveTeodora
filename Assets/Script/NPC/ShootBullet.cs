using PurrNet;
using UnityEngine;

public class ShootBullet : NetworkBehaviour
{
    Rigidbody2D rb;
    public float bulletSpeed = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * bulletSpeed;
    }



    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
}
