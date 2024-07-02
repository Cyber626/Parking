using UnityEngine;
using YG;

public class RewardAdsManager : MonoBehaviour
{
    public YandexGame sdk;
    public Game gameScript;
    public GameObject adsMenu;

    public void AdMenuButton()
    {
        adsMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseAdMenu()
    {
        adsMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void AdButton()
    {
        sdk._RewardedShow(1);
    }

    public void AdButtonCul()
    {
        Debug.Log("AdButtonCul");
        adsMenu.SetActive(false);
        gameScript.WinGame();
    }
}
