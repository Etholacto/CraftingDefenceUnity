using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldController : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    private PlayerPos setter;

    private float currentTime = 0f;
    private float startingTime = 30f;

    private float enemyAmount = 2f;
    private EnemyController enemy;

    [SerializeField] private TMP_Text countdownText;

    private void Start()
    {
        currentTime = startingTime;

        if (PlayerPrefs.GetString("IsCoop").Contains("yes"))
        {
            Vector3 P2Position = player1.transform.position + new Vector3(2, 0, 0);
            Instantiate(player2, P2Position, Quaternion.identity);
            setter.SetBool(false);
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
        else
        {
            enemy.SpawnEnemies(enemyAmount);
        }
        if (enemy.enemyAlive() <= 0)
        {
            enemyAmount = enemyAmount * 2;
            currentTime = startingTime;
        }
    }
}
