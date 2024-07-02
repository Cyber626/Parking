using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu, selectLevelMenu;

    public GameObject[] levelLockers;
    private int LevelsUnlocked;

    public void SelectLevel()
    {
        LevelsUnlocked = YandexGame.savesData.level;
        mainMenu.SetActive(false);
        selectLevelMenu.SetActive(true);
        for (int i = 0; i < LevelsUnlocked; i++)
        {
            levelLockers[i].SetActive(false);
        }
    }

    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        selectLevelMenu.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(LevelsUnlocked + 1);
    }

}
