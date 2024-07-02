using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Pause : MonoBehaviour
{

    [SerializeField]
    GameObject pauseMenu, dialogueMenu;
    public bool isDialogueShowedOnStart = false;
    public Game gameScript;
    private bool isDialogueShown;

    private void Start()
    {
        if (isDialogueShowedOnStart)
        {
            isDialogueShown = YandexGame.savesData.isDialogueShown;
            if (!isDialogueShown)
            {
                OpenDialogue();
            }
        }
    }

    public void OpenDialogue()
    {
        dialogueMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseDialogue()
    {
        dialogueMenu.SetActive(false);
        YandexGame.savesData.isDialogueShown = true;
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameScript.currentLevel + 1);
        YandexGame.SaveProgress();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameScript.currentLevel);
        YandexGame.SaveProgress();
    }

    public void ToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        YandexGame.SaveProgress();
    }

}
