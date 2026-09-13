using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject creditPanel;
    
    public void PlayGame()
    {
        SceneManager.LoadScene("level 1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpensCredits()
    {
        creditPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        creditPanel.SetActive(false);
    }
}
