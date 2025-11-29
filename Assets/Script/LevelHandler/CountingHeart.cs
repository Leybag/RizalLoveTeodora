using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountingHeart : MonoBehaviour
{

    [SerializeField] LevelHandler levelHandler;

    [Range(1, 3)]
    [SerializeField] int heartCountAssigned = 1;

    private void OnEnable()
    {
        if(levelHandler.heartCollected >= heartCountAssigned)
        {
            StartCoroutine(heartPop());
        }
        
    }

    IEnumerator heartPop()
    {
        yield return new WaitForSecondsRealtime(0.2f * heartCountAssigned);
        GetComponent<Animator>().Play("Pop");
    }
}
