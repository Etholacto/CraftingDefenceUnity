using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public bool isPanelActive()
    {
        return PausePanel.activeSelf;
    }
}
