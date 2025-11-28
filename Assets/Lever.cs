using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Lever : MonoBehaviour
{
    bool activated = false;

    public Transform LeverTransform;
    public Transform platform; 
    public Transform endPositionPlatform;
    public float Speed = 1.0f;

    Vector2 startPos;
    Vector2 EndPos;

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
            LeverTransform.rotation = Quaternion.RotateTowards(LeverTransform.rotation, Quaternion.Euler(0, 0, -60), 3f);
        }
        else
        {
            platform.transform.position = Vector2.MoveTowards(platform.position, startPos, Speed * Time.deltaTime);
            LeverTransform.rotation = Quaternion.RotateTowards(LeverTransform.rotation, Quaternion.Euler(0, 0, 60), 3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        activated = !activated;
    }

}
