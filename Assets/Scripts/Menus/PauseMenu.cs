﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {

	public static bool gameIsPaused = false;

	public Transform player;
	public Transform cam;
	private Vector3 playerStart;
	
	public GameObject pauseMenuUI;

	public Selectable PauseMenuStart;

	void Start() {
		Cursor.visible = false;
		playerStart = player.position;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Start"))
		{
			if (gameIsPaused)
			{
				resume();
			}
			else
			{
				pause();
			}
		}
		if (gameIsPaused && Input.GetButtonDown("Cancel"))
		{
			resume();
		}
	}

	public void resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		Cursor.visible = false;
		gameIsPaused = false;
	}

	void pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		Cursor.visible = true;
		gameIsPaused = true;
		PauseMenuStart.Select();
	}

	public void saveGame()
	{
		SaveGame.Instance.playerPosition = player.position;
		SaveGame.Instance.cameraPosition = cam.position;
		SaveGame.Save();
		Debug.Log("Saving Game...");
	}

	public void reloadLevel()
	{
		resume();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void loadSave()
	{
		SaveGame.Load();
		player.position = SaveGame.Instance.playerPosition;
		cam.position = SaveGame.Instance.cameraPosition;
		Debug.Log("Loading Save...");
	}

	public void loadMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("MainMenu");
		
		Debug.Log("Loading Menu...");
	}

	public void loadOverworld()
	{
		LoadScene sceneLoader = new LoadScene();
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		gameIsPaused = false;
		sceneLoader.LoadOverworld();
	}

	public void quitGame()
	{
		Debug.Log("Quitting Game...");
		
		Application.Quit();
	}
}
