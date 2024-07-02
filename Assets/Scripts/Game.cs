using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Game : MonoBehaviour
{
    [SerializeField]
    public int parkingAngle = 360;
    public Timer timerScript;
    public Pause pauseScript;
    public int currentLevel;
    public TextMeshProUGUI levelText, looseInfoText, winTimeLeft;
    public GameObject winMenu, looseMenu;
    public AudioSource crashFX;

    [Header("Moving car 1")]
    public bool isMovingCarExists = false;
    public int numberOfCars = 1;
    public int distanceRangeStart = 4, distanceRangeEnd = 8;
    public GameObject[] carPrefabs;
    public Vector3 startPostition, rotation, endPosition;
    public float speed = 1f;

    [Header("Moving car 2")]
    public bool isMovingCar2Exists = false;
    public int numberOfCars2 = 1;
    public int distanceRangeStart2 = 4, distanceRangeEnd2 = 8;
    public Vector3 startPostition2, rotation2, endPosition2;
    public float speed2 = 1f;

    private List<GameObject> movingCars = new List<GameObject>(), movingCars2 = new List<GameObject>();
    private Vector3 movingCarDirection, direction, movingCarDirection2, direction2;
    private CarSteering carSteeringScript;
    private int randomDistance, randomDistance2;
    Rigidbody2D rb;

    void Start()
    {
        levelText.text = currentLevel.ToString();
        rb = GetComponent<Rigidbody2D>();
        carSteeringScript = gameObject.GetComponent<CarSteering>();
        if (isMovingCarExists)
        {
            randomDistance = UnityEngine.Random.Range(distanceRangeStart, distanceRangeEnd);
            movingCars.Add(Instantiate(carPrefabs[UnityEngine.Random.Range(0, carPrefabs.Length)], startPostition, Quaternion.Euler(rotation)));

            direction = endPosition - startPostition;
            float magnitude = (float)Math.Sqrt(direction.x * direction.x + direction.y * direction.y + direction.z * direction.z);
            movingCarDirection = new Vector3(direction.x / magnitude, direction.y / magnitude, direction.z / magnitude);
        }

        if (isMovingCar2Exists)
        {
            randomDistance2 = UnityEngine.Random.Range(distanceRangeStart2, distanceRangeEnd2);
            movingCars2.Add(Instantiate(carPrefabs[UnityEngine.Random.Range(0, carPrefabs.Length)], startPostition2, Quaternion.Euler(rotation2)));

            direction2 = endPosition2 - startPostition2;
            float magnitude = (float)Math.Sqrt(direction2.x * direction2.x + direction2.y * direction2.y + direction2.z * direction2.z);
            movingCarDirection2 = new Vector3(direction2.x / magnitude, direction2.y / magnitude, direction2.z / magnitude);
        }
    }

    private void FixedUpdate()
    {
        if (isMovingCarExists)
        {
            if (movingCars.Count != numberOfCars)
            {
                Vector3 distance = movingCars[movingCars.Count - 1].transform.position - startPostition;
                double distanceLength = Math.Sqrt(distance.x * distance.x + distance.y * distance.y + distance.z * distance.z);
                if (distanceLength > randomDistance)
                {
                    movingCars.Add(Instantiate(carPrefabs[UnityEngine.Random.Range(0, carPrefabs.Length)], startPostition, Quaternion.Euler(rotation)));
                    randomDistance = UnityEngine.Random.Range(distanceRangeStart, distanceRangeEnd);
                }
            }
            for (int i = 0; i < movingCars.Count; i++)
            {
                movingCars[i].GetComponent<Transform>().Translate(speed * Time.fixedDeltaTime * movingCarDirection, Space.World);
            }
            // transforms[i].Rotate(new Vector3(-1, 0, 0) * rotateSpeed * Time.deltaTime);

            if (movingCars.Count != 0)
            {

                bool passed = Vector3.Dot(direction.normalized, (movingCars[0].GetComponent<Transform>().position - endPosition).normalized) > 0;
                if (passed)
                {
                    Destroy(movingCars[0]);
                    movingCars.RemoveAt(0);
                }
            }

        }

        if (isMovingCar2Exists)
        {
            if (movingCars2.Count != numberOfCars2)
            {
                Vector3 distance = movingCars2[movingCars2.Count - 1].transform.position - startPostition2;
                double distanceLength = Math.Sqrt(distance.x * distance.x + distance.y * distance.y + distance.z * distance.z);
                if (distanceLength > randomDistance2)
                {
                    movingCars2.Add(Instantiate(carPrefabs[UnityEngine.Random.Range(0, carPrefabs.Length)], startPostition2, Quaternion.Euler(rotation2)));
                    randomDistance2 = UnityEngine.Random.Range(distanceRangeStart2, distanceRangeEnd2);
                }
            }

            for (int i = 0; i < movingCars2.Count; i++)
            {
                movingCars2[i].GetComponent<Transform>().Translate(speed2 * Time.fixedDeltaTime * movingCarDirection2, Space.World);
            }

            if (movingCars2.Count != 0)
            {
                bool passed = Vector3.Dot(direction2.normalized, (movingCars2[0].GetComponent<Transform>().position - endPosition2).normalized) > 0;
                if (passed)
                {
                    Destroy(movingCars2[0]);
                    movingCars2.RemoveAt(0);
                }
            }
        }
    }

    private int prevParkingZoneCount = 0;

    private void OnCollisionEnter2D(Collision2D other)
    {
        LooseGame(true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (rb.velocity.magnitude < 0.01)
        {

            if (other.name == "ParkingZone")
            {
                if (prevParkingZoneCount == 4)
                {
                    if (parkingAngle >= 360)
                    {
                        WinGame();
                    }
                    else
                    {
                        var parkingRotation = gameObject.transform.eulerAngles;
                        var a = parkingRotation.z - parkingAngle;
                        a = (a + 180) % 360 - 180;

                        if (Math.Abs(a) < 45)
                        {
                            WinGame();
                        }
                        else
                        {
                            YandexGame.savesData.isDialogueShown = false;
                            pauseScript.RestartGame();
                        }
                    }
                }
                else
                {
                    prevParkingZoneCount++;
                }
            }
            else
            {
                prevParkingZoneCount = 0;
            }
        }
    }

    public void WinGame()
    {
        float currentTime = timerScript.TimeLeft;
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        winTimeLeft.text = string.Format("{0:00} : {1:00}", minutes, seconds);

        winMenu.SetActive(true);
        timerScript.TimerOn = false;
        if (YandexGame.savesData.level < currentLevel)
        {
            if (SceneManager.sceneCountInBuildSettings > currentLevel + 2)
            {
                YandexGame.savesData.level = currentLevel;
            }
        }
        FinishGame();
    }

    public void LooseGame(bool isCollisionHappened)
    {
        looseMenu.SetActive(true);
        if (isCollisionHappened)
        {
            if (YandexGame.savesData.isSoundOn)
            {
                crashFX.Play();
            }
            timerScript.TimerOn = false;
        }
        else
        {
            switch (YandexGame.EnvironmentData.language)
            {
                case "en": looseInfoText.text = "Time is up"; break;
                case "ru": looseInfoText.text = "Время истекло"; break;
                case "tr": looseInfoText.text = "Zaman doldu"; break;
                case "uz": looseInfoText.text = "Vaqt tugadi"; break;
            }
        }
        FinishGame();
    }

    public void FinishGame()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        this.enabled = false;
        carSteeringScript.enabled = false;
    }
}
