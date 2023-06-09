using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject Hud;

    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text mobsKilled;

    [SerializeField]
    private AudioManager AudioManager;
    [SerializeField] private AudioClip GameOverClip;

    public void Pause()
    {
        if (AudioManager != null)
        {
            AudioManager.StopAll();
            AudioManager.PlaySFX(GameOverClip);
        }
        WorldController classAInstance = FindObjectOfType<WorldController>();
        GameOverPanel.SetActive(true);
        Hud.SetActive(false);
        Time.timeScale = 0;

        levelText.text = "Levels passed: " + classAInstance.gameLevel.ToString("0");
        mobsKilled.text = "Mobs killed: " + classAInstance.mobsKilled_.ToString("0");
    }

    public void Continue()
    {
        GameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void restart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1;
    }

    public bool isPanelActive()
    {
        return GameOverPanel.activeSelf;
    }
}
