using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] int level = 1;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] GameObject lockGameObject;
    [SerializeField] Image image;

    [SerializeField] Sprite starCollectedSprite;

    [SerializeField] Image Star1;
    [SerializeField] Image Star2;
    [SerializeField] Image Star3;

    bool unlock = false;

    void Start()
    {
        unlock = GameManager.CheckLevelUnlock(level);

        levelText.text = level.ToString();

        if (unlock)
        {
            image.color = Color.white;
            lockGameObject.SetActive(false);

            int unlockedHearts = GameManager.CheckLevelHeartCollected(level);

            if( unlockedHearts >= 1)
            {
                Star1.sprite = starCollectedSprite;
            }
            if (unlockedHearts >= 2)
            {
                Star2.sprite = starCollectedSprite;
            }
            if (unlockedHearts >= 3)
            {
                Star3.sprite = starCollectedSprite;
            }
        }
        else
        {
            image.color = Color.grey;
            lockGameObject.SetActive(true);
        }

    }
    
    public void levelButtonPressed()
    {
        if (unlock)
        {
            SceneManager.LoadScene("Level "+ level);
        }
    }
}
