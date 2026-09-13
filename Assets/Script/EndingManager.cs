using UnityEngine;
using TMPro;
using System.Collections;

public class EndingManager : MonoBehaviour
{
    public GameObject endingPanel;
    public TMP_Text endingText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartEnding()
    {
        endingPanel.SetActive(true);
        StartCoroutine(EndingSequence());
    }

    // Update is called once per frame
    IEnumerator EndingSequence()
    {
        endingText.text = "YOU THOUGHT THEY WERE TWO.";
        yield return new WaitForSeconds(3f);

        endingText.text = "BUT THERE WAS ONLY ONE SOUL.";
        yield return new WaitForSeconds(3f);

        endingText.text = "THE END";
    }

    public void MainMenu()
    {
    Time.timeScale = 5f;
    UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
