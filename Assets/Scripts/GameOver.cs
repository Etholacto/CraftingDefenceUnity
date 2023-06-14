using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	[SerializeField] GameObject GameOverPanel;
	[SerializeField] GameObject Hud;

	public void Pause()
	{
		GameOverPanel.SetActive(true);
		Hud.SetActive(false);
		Time.timeScale = 0;
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
