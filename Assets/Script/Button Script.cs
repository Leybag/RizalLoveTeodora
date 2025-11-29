using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject OptionPanel;
    public void Option()
    {
        OptionPanel.SetActive(true);
    }

    public void OptionExit()
    {
        OptionPanel.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("Levels Menu");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
