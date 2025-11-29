using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    bool activated = false;

    public Transform buttonHead;
    public Transform platform; 
    public Transform endPositionPlatform;
    public float Speed = 1.0f;

    Vector2 startPos;
    Vector2 EndPos;
    List<Transform> insideCollider = new List<Transform>();

    private void Start()
    {
        startPos = platform.position;
        EndPos = endPositionPlatform.position;
        Destroy(endPositionPlatform.gameObject);
    }

    private void Update()
    {
        if (activated)
        {
            platform.transform.position = Vector2.MoveTowards(platform.position, EndPos, Speed * Time.deltaTime);
            
        }
        else
        {
            platform.transform.position = Vector2.MoveTowards(platform.position, startPos, Speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        activated = true;
        buttonHead.localPosition = new Vector3(0, 0.05f, 0);
        insideCollider.Add(collision.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (insideCollider.Contains(collision.transform))
        {
            insideCollider.Remove(collision.transform);
            if(insideCollider.Count <= 0)
            {
                activated = false;
                buttonHead.localPosition = new Vector3(0, 0.1f, 0);
            }
        }
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
