using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    List<Transform> onPlatformTransforms = new List<Transform>();

    Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        Vector3 deltaPosition = transform.position - lastPosition;
        lastPosition = transform.position;
        foreach (Transform t in onPlatformTransforms)
        {
            t.position += deltaPosition;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Player" || LayerMask.LayerToName(collision.gameObject.layer) == "Moveable" || LayerMask.LayerToName(collision.gameObject.layer) == "Enemy" || LayerMask.LayerToName(collision.gameObject.layer) == "NPC")
        {
            onPlatformTransforms.Add(collision.transform);
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Player" || LayerMask.LayerToName(collision.gameObject.layer) == "Moveable" || LayerMask.LayerToName(collision.gameObject.layer) == "Enemy" || LayerMask.LayerToName(collision.gameObject.layer) == "NPC")
        {
            if (onPlatformTransforms.Contains(collision.transform))
            {
                onPlatformTransforms.Remove(collision.transform);
            }
        }
    }
}
