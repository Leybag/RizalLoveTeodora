using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rizal : MonoBehaviour
{
    [SerializeField] LevelHandler levelHandler;

    [SerializeField] float SPEED = 3f;
    [SerializeField] float JUMPSTRENGTH = 6f;

    Rigidbody2D rb;

    Vector2 velocity = Vector2.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement();
    }

    void movement()
    {
        if (levelHandler != null && !levelHandler.levelEnd)
        {
            float inputHorizontal = Input.GetAxis("Horizontal");

            velocity.x = inputHorizontal * SPEED;
            if (Input.GetButtonDown("Vertical")) // jump button
            {
                velocity.y = JUMPSTRENGTH;
            }
            else
            {
                velocity.y = rb.velocity.y;
            }
            rb.velocity = velocity;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (levelHandler != null && !levelHandler.levelEnd)
        {
            if (collision.transform.CompareTag("Player"))
            {
                levelHandler.levelFinished();
            }
            if (collision.transform.CompareTag("Enemy"))
            {
                levelHandler.levelFailed();
            }
        }
    }
}
