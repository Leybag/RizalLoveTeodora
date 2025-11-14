using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{

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
        float inputHorizontal = Input.GetAxis("Horizontal");

        velocity.x = inputHorizontal * SPEED;

        if (Input.GetButtonDown("Vertical"))
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
