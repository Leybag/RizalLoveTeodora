using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    //level cant be zero
    [SerializeField] int level = 1;

    [SerializeField] Animator Panelheart1;
    [SerializeField] Animator Panelheart2;
    [SerializeField] Animator Panelheart3;

    //temporary
    [SerializeField] GameObject VictoryPanel;
    [SerializeField] GameObject FailedPanel;

    int heartCollected = 0;
    public bool levelEnd = false;

    public void addHeart()
    {
        heartCollected++;
        switch (heartCollected)
        {
            case 1:
                Panelheart1.Play("Pop");
                break;
            case 2:
                Panelheart2.Play("Pop");
                break;
            case 3:
                Panelheart3.Play("Pop");
                break;
        }
    }

    public void levelFinished()
    {
        if (!levelEnd)
        {
            levelEnd = true;
            GameManager.setLevelCollectedHearts(level, heartCollected);
            GameManager.UnlockLevel(level+1);

            //Change this later
            VictoryPanel.SetActive(true);
        }
        
    }

    public void levelFailed()
    {
        if (!levelEnd)
        {
            levelEnd = true;
            GameManager.setLevelCollectedHearts(level, heartCollected);

            //Change this later
            FailedPanel.SetActive(true);
        }
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level " + (level +1));
    }
}
