using UnityEngine;
using UnityEngine.UI;
using YG;

public class BackgroundMusicScript : MonoBehaviour
{
    public static BackgroundMusicScript instance;
    public Image musicButton;
    public Sprite onImage, offImage;
    public Image soundButton;
    public Sprite soundOnImage, soundOffImage;
    [HideInInspector]
    public bool isSoundOn, isMusicOn;
    public AudioSource engineStart;
    public CarSteering carSteering;
    AudioSource backgroudnAudio;

    private void Start()
    {
        isSoundOn = YandexGame.savesData.isSoundOn;
        if (isSoundOn)
        {
            soundButton.sprite = soundOnImage;
            carSteering.EngineSoundOnOff(true);
        }
        else
        {
            engineStart.enabled = false;
            soundButton.sprite = soundOffImage;
            carSteering.EngineSoundOnOff(false);
        }

        isMusicOn = YandexGame.savesData.isMusicOn;
        backgroudnAudio = GetComponent<AudioSource>();
        if (isMusicOn)
        {
            backgroudnAudio.enabled = true;
            musicButton.sprite = onImage;
        }
        else
        {
            backgroudnAudio.enabled = false;
            musicButton.sprite = offImage;
        }

    }

    public void MusicOnOff()
    {
        if (isMusicOn)
        {
            musicButton.sprite = offImage;
            backgroudnAudio.enabled = false;
            isMusicOn = false;
        }
        else
        {
            musicButton.sprite = onImage;
            backgroudnAudio.enabled = true;
            isMusicOn = true;
        }
        YandexGame.savesData.isMusicOn = isMusicOn;
    }

    public void SoundOnOff()
    {
        if (isSoundOn)
        {
            carSteering.EngineSoundOnOff(false);
            soundButton.sprite = soundOffImage;
            isSoundOn = false;
        }
        else
        {
            carSteering.EngineSoundOnOff(true);
            soundButton.sprite = soundOnImage;
            isSoundOn = true;
        }
        YandexGame.savesData.isSoundOn = isSoundOn;
    }
}
