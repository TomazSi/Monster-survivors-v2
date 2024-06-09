using System;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
	public GameObject levelUpUI;
	public static bool GameIsPaused = false;

	void Start()
	{
		levelUpUI.SetActive(false); // Ensure the level-up UI is hidden initially
	}

	// Stop game
	public void ShowLevelUpMenu()
	{
		levelUpUI.SetActive(true);
		PauseGame();
	}

	// Resume game
	public void HideLevelUpMenu()
	{
		levelUpUI.SetActive(false);
        ResumeGame();
	}

	void PauseGame()
	{
		Time.timeScale = 0f; // Pauses anything dependent on Time.timeScale
		GameIsPaused = true;
	}

	void ResumeGame()
	{
		Time.timeScale = 1f;
		GameIsPaused = false;
	}
}
