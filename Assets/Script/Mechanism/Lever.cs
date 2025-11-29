using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Lever : MonoBehaviour
{
    bool activated = false;

    SpriteRenderer LeverSpriteRenderer;
    public Sprite LeverActivatedSprite;
    public Sprite LeverDeactivatedSprite;

    public Transform platform; 
    public Transform endPositionPlatform;
    public float Speed = 1.0f;

    Vector2 startPos;
    Vector2 EndPos;

    private void Start()
    {
        LeverSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        startPos = platform.position;
        EndPos = endPositionPlatform.position;
        Destroy(endPositionPlatform.gameObject);
    }

    private void Update()
    {
        if (activated)
        {
            platform.transform.position = Vector2.MoveTowards(platform.position, EndPos, Speed * Time.deltaTime);
            LeverSpriteRenderer.sprite = LeverActivatedSprite;
        }
        else
        {
            platform.transform.position = Vector2.MoveTowards(platform.position, startPos, Speed * Time.deltaTime);
            LeverSpriteRenderer.sprite = LeverDeactivatedSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        activated = !activated;
    }
    private void OnDrawGizmos()
    {
        if (endPositionPlatform != null)
        {
            Gizmos.DrawLine(platform.position, endPositionPlatform.position);
        }
        else
        {
            Gizmos.DrawLine(startPos, EndPos);
        }
    }
}
