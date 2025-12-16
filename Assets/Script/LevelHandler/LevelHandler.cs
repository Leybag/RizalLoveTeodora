using PurrNet;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : NetworkBehaviour
{
    //level cant be zero
    [SerializeField] int level = 1;

    [SerializeField] Animator Panelheart1;
    [SerializeField] Animator Panelheart2;
    [SerializeField] Animator Panelheart3;

    //temporary
    [SerializeField] GameObject VictoryPanel;
    [SerializeField] GameObject FailedPanel;
    [SerializeField] GameObject MenuPanel;

    [NonSerialized] public int heartCollected = 0;
    public bool levelEnd = false;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Menubtn();
        }
    }

    [ObserversRpc]
    public void addHeart(GameObject heart)
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
        Destroy(heart);
    }

    [ObserversRpc]
    public void levelFinished()
    {
        if (!levelEnd)
        {
            levelEnd = true;
            GameManager.setLevelCollectedHearts(level, heartCollected);
            GameManager.UnlockLevel(level+1);
            Time.timeScale = 0f;

            //Change this later
            VictoryPanel.SetActive(true);
        }

        else
        {
            VictoryPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        
    }

    [ObserversRpc]
    public void levelFailed()
    {
        if (!levelEnd)
        {
            levelEnd = true;
            GameManager.setLevelCollectedHearts(level, heartCollected);
            Time.timeScale = 0f;

            //Change this later
            FailedPanel.SetActive(true);
        }
    }

    [ObserversRpc]
    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    [ObserversRpc]
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }


    [ObserversRpc]
    public void NextLevel()
    {
        SceneManager.LoadScene("Level " + (level +1));
        Time.timeScale = 1f;
    }

    [ObserversRpc]
    public void Menubtn()
    {
        MenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    [ObserversRpc]
    public void Resumebtn()
    {
        MenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    
    
}
