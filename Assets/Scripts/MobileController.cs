using UnityEngine;
using UnityEngine.UI;
using YG;

public class MobileController : MonoBehaviour
{
    public GameObject controllers;
    public Button controllerButton;
    public Sprite controllerOn, controllerOff;

    private bool gas = false, brake = false, left = false, right = false;
    public int vertical, horizontal;

    private bool isControllerOn;

    private void Start()
    {
        isControllerOn = YandexGame.savesData.isControllerOn;
        if (!isControllerOn)
        {
            controllers.SetActive(false);
        }
    }

    private void Update()
    {
        if (left)
        {
            horizontal = 1;
        }
        else if (right)
        {
            horizontal = -1;
        }
        else
        {
            horizontal = 0;
        }

        if (gas)
        {
            vertical = 1;
        }
        else if (brake)
        {
            vertical = -1;
        }
        else
        {
            vertical = 0;
        }
    }

    public void ButtonDown(int val)
    {
        switch (val)
        {
            case 0: gas = true; break;
            case 1: brake = true; break;
            case 2: left = true; break;
            case 3: right = true; break;
        }
    }
    public void ButtonUp(int val)
    {
        switch (val)
        {
            case 0: gas = false; break;
            case 1: brake = false; break;
            case 2: left = false; break;
            case 3: right = false; break;
        }
    }

    public void toggleControllerButton()
    {
        if (isControllerOn)
        {
            isControllerOn = false;
        }
        else
        {
            isControllerOn = true;
        }

        controllers.SetActive(isControllerOn);

        YandexGame.savesData.isControllerOn = isControllerOn;
    }
}
