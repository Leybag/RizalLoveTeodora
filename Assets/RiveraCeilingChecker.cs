using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiveraCeilingChecker : MonoBehaviour
{
    Rivera rivera;

    List<Transform> ceilingTransforms = new List<Transform>();

    private void Start()
    {
        rivera = transform.parent.GetComponent<Rivera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ceilingTransforms.Add(collision.transform);
        rivera.isThereCeiling = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ceilingTransforms.Contains(collision.transform))
        {
            ceilingTransforms.Remove(collision.transform);
            if (ceilingTransforms.Count <= 0)
            {
                rivera.isThereCeiling = false;
            }
        }
    }
}
