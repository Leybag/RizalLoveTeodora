using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rivera : MonoBehaviour
{
    LevelHandler levelHandler;

    [SerializeField] float SPEED = 3f;
    [SerializeField] float JUMPSTRENGTH = 6f;

    Rigidbody2D rb;

    Vector2 velocity = Vector2.zero;
    [SerializeField] Transform groundCheckPos;
    [SerializeField] LayerMask groundLayerMask;
    bool onGround = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
        if (levelHandler != null && !levelHandler.levelEnd)
        {
            velocity = rb.velocity; // don't remove

            onGround = Physics2D.OverlapBox(groundCheckPos.position, new Vector2(.25f, .01f), 0, groundLayerMask) != null;

            float inputHorizontal = Input.GetAxis("Horizontal1");
            velocity.x = inputHorizontal * SPEED;



            if(onGround && Input.GetAxisRaw("Vertical1") > 0) // Jump button
            {
                velocity.y = JUMPSTRENGTH;
            }
            else if (!onGround && velocity.y < -.2f && Input.GetAxisRaw("Vertical1") > 0) // Glide
            {
                velocity.y = -0.2f;
            }
            else
            {
                velocity.y = rb.velocity.y;
            }
            
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
}
