using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rivera : MonoBehaviour
{
    [NonSerialized] public LevelHandler levelHandler;

    [SerializeField] float SPEED = 3f;
    [SerializeField] float JUMPSTRENGTH = 6f;

    Rigidbody2D rb;

    public Vector2 velocity = Vector2.zero;
    [SerializeField] Transform groundCheckPos;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] SpriteRenderer sprite;
    Animator anim;
    bool onGround = false;
    [NonSerialized] public bool isThereCeiling = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject[] rootObj = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in rootObj)
        {
            if(levelHandler == null)
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
        velocity = rb.velocity; // don't remove

        onGround = Physics2D.OverlapBox(groundCheckPos.position, new Vector2(.25f, .01f), 0, groundLayerMask) != null;

        float inputHorizontal = Input.GetAxisRaw("Horizontal1");
        float inputVertical = Input.GetAxisRaw("Vertical1");

        velocity.x = inputHorizontal * SPEED;



        if (onGround && inputVertical > 0) // Jump button
        {
            velocity.y = JUMPSTRENGTH;
        }
        else if (!onGround && velocity.y < -.3f && inputVertical > 0 && !isThereCeiling) // Glide
        {
            velocity.y = -0.3f;
        }
        else
        {
            velocity.y = rb.velocity.y;
        }
        animationHaldler(inputHorizontal, inputVertical);
        rb.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
    }

    void animationHaldler(float HorizontalInput , float VerticalInput)
    {
        if (HorizontalInput > 0)
        {
            sprite.flipX = false;
        }
        else if (HorizontalInput < 0)
        {
            sprite.flipX = true;
        }

        if (!isThereCeiling)
        {
            if (HorizontalInput == 0 && onGround)
            {
                anim.Play("Idle");
            }
            else if (HorizontalInput != 0 && onGround)
            {
                anim.Play("Walk");
            }
            else if (!onGround && VerticalInput > 0 && rb.velocity.y < 0.29f)
            {
                anim.Play("Glide");
            }
            else if (!onGround && rb.velocity.y > 0.29f)
            {
                anim.Play("Jump");
            }
        }
        else
        {
            if (HorizontalInput == 0)
            {
                anim.Play("Crouch_Idle");
            }
            else if (HorizontalInput != 0)
            {
                anim.Play("Crouch");
            }
        }

        
    }

}
