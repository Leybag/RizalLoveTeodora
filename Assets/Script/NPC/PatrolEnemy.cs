using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public float SPEED = 2f;
    Animator anim;

    [SerializeField] Transform flipDirection;
    [SerializeField] Transform checkGroundPosition;
    [SerializeField] Transform checkWallPosition;
    [SerializeField] Transform checkWallPosition2;
    [SerializeField] LayerMask ObstacleMask;

    [SerializeField] Transform bulletSpawnPosition;
    [SerializeField] GameObject BulletPrefab = null;
    [SerializeField] float bulletShootInterval = 1f;
    [SerializeField] float bulletSpeed = 1f;

    Rigidbody2D rb;

    bool isThereGround = true;
    bool isThereWall = true;
    bool isShooting = false;
    float shootTimer = 0f;
    int direction = 1;
    Vector2 velocity = Vector2.zero;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        velocity = rb.velocity;

        if (!isShooting)
        {
            checkIfChangeDirection();

            velocity.x = SPEED * direction;

            if(BulletPrefab != null)
            {
                shootTimer += Time.deltaTime;
                if (shootTimer >= bulletShootInterval)
                {
                    isShooting = true;
                    anim.Play("Shoot");
                }
                else
                {
                    anim.Play("Walk");
                }
            }
        }
        else
        {
            velocity.x = 0;
        }

        rb.velocity = velocity;
    }

    public void Shoot()
    {
        isShooting = false;
        shootTimer = 0f;
        ShootBullet b = Instantiate(BulletPrefab, bulletSpawnPosition.position, Quaternion.identity).GetComponent<ShootBullet>();
        b.bulletSpeed = (bulletSpeed + SPEED) * direction;
    }

    void changeDirection()
    {
        direction *= -1;
        flipDirection.localScale = new Vector3(1 * direction,1,1);
    }

    void checkIfChangeDirection()
    {
        isThereGround = Physics2D.Raycast(checkGroundPosition.position, Vector2.down, .2f, ObstacleMask);
        isThereWall = Physics2D.Raycast(checkWallPosition.position, Vector2.right * direction, .40f, ObstacleMask);

        if (!isThereWall)
        {
            isThereWall = Physics2D.Raycast(checkWallPosition2.position, Vector2.right * direction, .40f, ObstacleMask);
        }

        if (!isThereGround || isThereWall)
        {
            changeDirection();
        }
    }
}
