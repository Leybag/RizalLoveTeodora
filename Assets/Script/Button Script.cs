using PurrNet;
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
        NetworkManager.main.sceneModule.LoadSceneAsync("Levels Menu");
    }

    public void MainMenu()
    {
        NetworkManager.main.sceneModule.LoadSceneAsync("Main Menu");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Set_CreatePort()
    {
        NetworkManager.main.StartHost();
        NetworkManager.main.sceneModule.LoadSceneAsync("Levels Menu");
    }

    public void Set_JoinPort()
    {
        NetworkManager.main.StartClient();
        NetworkManager.main.sceneModule.LoadSceneAsync("Levels Menu");
    }
}
