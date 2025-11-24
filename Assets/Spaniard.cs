using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaniard : MonoBehaviour
{
    public float SPEED = 2f;

    [SerializeField] Transform flipDirection;
    [SerializeField] Transform checkGroundPosition;
    [SerializeField] Transform checkWallPosition;
    [SerializeField] LayerMask groundMask;
    


    Rigidbody2D rb;

    bool isThereGround = true;
    bool isThereWall = true;
    public int direction = 1;
    Vector2 velocity = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        velocity = rb.velocity;

        isThereGround = Physics2D.Raycast(checkGroundPosition.position, Vector2.down, .2f,groundMask);
        isThereWall = Physics2D.Raycast(checkWallPosition.position, (Vector2.right * .3f) * direction, .2f, groundMask);

        if (!isThereGround)
        {
            changeDirection();
        }
        //if (isThereWall)
        //{
        //    changeDirection();
        //}

        velocity.x = SPEED * direction;

        rb.velocity = velocity;
    }

    void changeDirection()
    {
        direction *= -1;
        flipDirection.localScale = new Vector3(1 * direction,1,1);
    }
}
