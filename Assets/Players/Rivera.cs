using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rivera : MonoBehaviour
{
    [SerializeField] LevelHandler levelHandler;

    [SerializeField] float SPEED = 3f;
    [SerializeField] float JUMPSTRENGTH = 6f;

    Rigidbody2D rb;

    Vector2 velocity = Vector2.zero;
    [SerializeField] Transform groundCheckPos;
    [SerializeField] LayerMask groundLayerMask;
    [NonSerialized] public bool onGround = false;

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
        onGround = Physics2D.OverlapBox(groundCheckPos.position, new Vector2(1, .05f), groundLayerMask) != null; // edit this shit

        if (levelHandler != null && !levelHandler.levelEnd)
        {
            float inputHorizontal = Input.GetAxis("Horizontal1");

            velocity.x = inputHorizontal * SPEED;



            if(onGround && Input.GetButton("Vertical1")) // jump button
            {
                velocity.y = JUMPSTRENGTH;
            }
            else if (!onGround && rb.velocity.y < 0 && Input.GetButton("Vertical1")) // Glide
            {
                velocity.y = -.5f;
            }
            else
            {
                velocity.y = rb.velocity.y;
            }
            
            rb.velocity = velocity;
        }
        print(onGround);
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
