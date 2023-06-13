using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WorldController : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    private float currentTime = 0f;
    private float startingTime = 5f;

    private float enemyAmount = 20f;
    private bool oneTimeMusic = false;

    [SerializeField]
    private EnemyController enemy;

    [Header("Day-Night")]
    [SerializeField] private float startHour;
    [SerializeField] private float timeMultiplier;
    [SerializeField] private Light sunLight;
    [SerializeField] private float sunrise;
    [SerializeField] private float sunset;
    [SerializeField] private Color DayLight;
    [SerializeField] private Color NightLight;
    [SerializeField] private AnimationCurve lightChangeCurve;
    [SerializeField] private float MaxLumen;
    [SerializeField] private Light moonLight;
    [SerializeField] private float maxMoonLight;
    private TimeSpan sunriseTS;
    private TimeSpan sunsetTS;
    private DateTime time;


    [Header("Audio Clips")]
    [SerializeField] private AudioClip atackSound;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip battleSound;

    [SerializeField] private TMP_Text countdownText;

    private AudioManager AudioManager;

    private void Start()
    {
        currentTime = startingTime;
        if (PlayerPrefs.GetString("IsCoop").Contains("no"))
        {
            Destroy(player2);
        }

        time = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        sunriseTS = TimeSpan.FromHours(sunrise);
        sunsetTS = TimeSpan.FromHours(sunset);

        AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (currentTime > 0f)
        {

            currentTime -= 1 * Time.deltaTime;
            if (countdownText != null)
            {
                countdownText.text = currentTime.ToString("0");
            }
        }
        if (currentTime < 0.01f && currentTime > 0f)
        {
            if (oneTimeMusic)
            {
                AudioManager.PlaySFX(battleSound);

                oneTimeMusic = false;
            }
            enemy.SpawnEnemies(enemyAmount / 2);
            oneTimeMusic = true;

        }
        if (enemy != null && currentTime < -5f)
        {
            if (enemy.enemyAlive() <= 0)
            {
                enemyAmount = enemyAmount * 2;
                currentTime = startingTime;
            }
        }

        UpdateTime();
        RotateSun();
        UpdateLight();
    }

    private void UpdateTime()
    {
        time = time.AddSeconds(Time.deltaTime * timeMultiplier);
    }

    private void RotateSun()
    {
        float sunLightRotate;

        if (time.TimeOfDay > sunriseTS && time.TimeOfDay < sunsetTS)
        {
            TimeSpan sunriseToSunset = CalculateTimeDifference(sunriseTS, sunsetTS);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTS, time.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunset.TotalMinutes;

            sunLightRotate = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunrise = CalculateTimeDifference(sunsetTS, sunriseTS);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTS, time.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunrise.TotalMinutes;

            sunLightRotate = Mathf.Lerp(180, 360, (float)percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotate, Vector3.right);
    }

    private void UpdateLight()
    {
        float dotProd = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, MaxLumen, lightChangeCurve.Evaluate(dotProd));
        moonLight.intensity = Mathf.Lerp(maxMoonLight, 0, lightChangeCurve.Evaluate(dotProd));
        RenderSettings.ambientLight = Color.Lerp(NightLight, DayLight, lightChangeCurve.Evaluate(dotProd));
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan diff = toTime - fromTime;

        if (diff.TotalSeconds < 0)
        {
            diff += TimeSpan.FromHours(24);
        }

        return diff;
    }
}
