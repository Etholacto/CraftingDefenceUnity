using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGameP1()
    {
        PlayerPrefs.SetString("IsCoop", "no");
        SceneManager.LoadScene("CastleMap");
    }

    public void PlayGameP2()
    {
        PlayerPrefs.SetString("IsCoop", "yes");
        SceneManager.LoadScene("CastleMap");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
