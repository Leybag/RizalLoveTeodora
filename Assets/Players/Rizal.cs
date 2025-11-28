using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Rizal : MonoBehaviour
{
    LevelHandler levelHandler;

    [SerializeField] float SPEED = 3f;
    [SerializeField] float CLIMBSPEED = 3f;
    [SerializeField] float JUMPSTRENGTH = 8f;

    Rigidbody2D rb;

    public Vector2 velocity = Vector2.zero;
    [SerializeField] LayerMask groundLayerMask;
    bool onGround = false;
    [SerializeField] LayerMask ladderLayerMask;
    bool onLadder = false;
    Collider2D ladderCol;
    Animator anim;
    [SerializeField] SpriteRenderer sprite;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject[] rootObj = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in rootObj)
        {
            if (levelHandler == null)
            {
                obj.TryGetComponent<LevelHandler>(out levelHandler);
            }
        }

    }

    private void Update()
    {
        movement();
    }

    void movement()
    {
        if (levelHandler != null && !levelHandler.levelEnd)
        {
            velocity = rb.velocity; // don't remove

            onGround = Physics2D.OverlapBox(transform.position, new Vector2(.25f, .01f), 0, groundLayerMask) != null;
            onLadder = (ladderCol = Physics2D.OverlapBox(transform.position + (Vector3.up * .5f), new Vector2(.25f, .01f), 0, ladderLayerMask)) != null;

        
            float inputHorizontal = Input.GetAxisRaw("Horizontal");
            float inputVertical = Input.GetAxisRaw("Vertical");

            velocity.x = inputHorizontal * SPEED;

            if (onGround && inputVertical > 0) // Jump button
            {
                velocity.y = JUMPSTRENGTH;
                //jump
            }
            else if (!onGround && onLadder) // Climb buttons
            {
                //climb
                velocity = Vector2.zero;
                float newXpos = transform.position.x;

                if(inputHorizontal == 0 && inputVertical != 0)
                {
                    newXpos = Mathf.Lerp(transform.position.x, ladderCol.bounds.center.x, .1f);
                }
                else if (inputHorizontal != 0)
                {
                    newXpos = transform.position.x + (inputHorizontal * SPEED) * Time.deltaTime;
                }
                transform.position = new Vector3(newXpos, transform.position.y + (inputVertical * CLIMBSPEED) * Time.deltaTime, 0);
            }
            else
            {
                velocity.y = rb.velocity.y;
            }
            if (onLadder)
            {
                rb.gravityScale = 0;
            }
            else
            {
                rb.gravityScale = 2;
            }
            
            animationHandler(inputHorizontal);

            rb.velocity = velocity;
        }
        else
        {
            rb.velocity = Vector2.zero;
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
            if (collision.transform.CompareTag("Danger"))
            {
                levelHandler.levelFailed();
            }
            if (collision.transform.CompareTag("Spaniard"))
            {
                levelHandler.levelFailed();
            }
            if (collision.transform.CompareTag("Friar"))
            {
                levelHandler.levelFailed();
            }
        }
        if (collision.gameObject.CompareTag("Platform"))
        {

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.parent = null;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Match the rotation
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(
            transform.position,
            Quaternion.Euler(0, 0, 0),
            Vector3.one
        );
        Gizmos.matrix = rotationMatrix;

        // Draw the box
        Gizmos.DrawWireCube(Vector3.zero, new Vector2(.25f, .01f));
        Gizmos.DrawWireCube(Vector3.up * .5f, new Vector2(.25f, .01f));
    }

    void animationHandler(float horizontalInput)
    {
        if (horizontalInput > 0)
        {
            sprite.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            sprite.flipX = true;
        }


        if (horizontalInput == 0 && onGround && !onLadder)
        {
            anim.Play("Idle");
        }
        else if(horizontalInput != 0 && onGround && !onLadder)
        {
            anim.Play("Walk");
        }
        else if (!onLadder && !onGround)
        {
            anim.Play("Jump");
        }
        else if (onLadder && !onGround && horizontalInput == 0)
        {
            anim.Play("Climb_Idle");
        }
        else if (onLadder && !onGround && horizontalInput != 0)
        {
            anim.Play("Climb");
        }
    }

}
