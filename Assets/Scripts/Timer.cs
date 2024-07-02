using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public float TimeLeft;
    public bool TimerOn = false;
    private TMP_Text TimerText;
    public Game gameScript;

    private void Start() {
        TimerOn = true;
        TimerText = gameObject.GetComponent<TMP_Text> ();
    }

    private void Update() {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                UpdateTimer(TimeLeft);
            }
            else
            {
                TimeLeft = 0;
                TimerOn = false;
                gameScript.LooseGame(false);
            }
        }    
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

}
