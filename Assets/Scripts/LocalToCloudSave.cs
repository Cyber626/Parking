using UnityEngine;
using YG;

public class LocalToCloudSave : MonoBehaviour
{

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Awake()
    {
        if (YandexGame.SDKEnabled)
            GetLoad();
    }

    private void GetLoad()
    {
        int localSavedLevel = PlayerPrefs.GetInt("level");
        if (YandexGame.savesData.level < localSavedLevel)
        {
            // YandexGame.LoadProgress();
            YandexGame.savesData.level = localSavedLevel;
            YandexGame.savesData.isMusicOn = PlayerPrefs.GetInt("bMusic", 1) == 1;
            YandexGame.savesData.isSoundOn = PlayerPrefs.GetInt("soundFX", 1) == 1;
            YandexGame.savesData.isDialogueShown = PlayerPrefs.GetInt("dialogue") == 0;
            YandexGame.savesData.isControllerOn = PlayerPrefs.GetInt("controller", 1) == 1;
            YandexGame.SaveProgress();
        }
    }
}
