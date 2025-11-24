using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rizal : MonoBehaviour
{
    LevelHandler levelHandler;

    [SerializeField] float SPEED = 3f;
    [SerializeField] float CLIMBSPEED = 3f;
    [SerializeField] float JUMPSTRENGTH = 8f;

    Rigidbody2D rb;

    Vector2 velocity = Vector2.zero;
    [SerializeField] LayerMask groundLayerMask;
    bool onGround = false;
    [SerializeField] LayerMask ladderLayerMask;
    bool onLadder = false;
    Collider2D ladderCol;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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

        
            float inputHorizontal = Input.GetAxis("Horizontal");
            float inputVertical = Input.GetAxis("Vertical");
            velocity.x = inputHorizontal * SPEED;

            if (onGround && inputVertical > 0) // Jump button
            {
                velocity.y = JUMPSTRENGTH;
            }
            else if (!onGround && onLadder) // Climb buttons
            {
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
}
