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
    private PlayerPos setter;

    private float currentTime = 0f;
    private float startingTime = 5f;

    private float enemyAmount = 20f;
    private bool oneTimeMusic = false;

    [SerializeField]
    private EnemyController enemy;
    private TowerBuyP2 tbp2;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip atackSound;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip battleSound;

    [SerializeField] private TMP_Text countdownText;

    private AudioManager AudioManager;

    private void Start()
    {
        currentTime = startingTime;
        if (PlayerPrefs.GetString("IsCoop").Contains("yes"))
        {
            //setter.SetBool(false);
            //tbp2.SetTowerSettings(player2.transform.GetChild(0).gameObject.transform, player2.transform.GetChild(0).gameObject.transform);
        }
        else
        {
            DestroyImmediate(player2, true);
        }
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
            if(oneTimeMusic) {
                AudioManager.PlaySFX(battleSound);

                oneTimeMusic = false;
            }
            enemy.SpawnEnemies(enemyAmount/2);
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
    }
}
