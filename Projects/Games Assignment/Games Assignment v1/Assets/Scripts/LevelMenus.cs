using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenus : MonoBehaviour		//Has to be attached to Main Camera GameObject
{
	public bool gameIsPaused = false;
	public bool gameIsWon = false;
	public bool gameIsLost = false;
	public GameObject pauseMenu;
	public GameObject winMenu;
	public GameObject loseMenu;

	public void resume(){
		pauseMenu.SetActive(false);
		winMenu.SetActive(false);
		loseMenu.SetActive(false);
		Time.timeScale = 1f;
		gameIsPaused = false;
		gameIsLost = false;
		gameIsWon = false;
	}

	public void pause(){
		pauseMenu.SetActive(true);
		Time.timeScale = 0f;
		gameIsPaused = true;
	}

    // Update is called once per frame
    void Update()
    {
		if(gameIsLost){
			loseMenu.SetActive(true);
			Time.timeScale = 0f;
			gameIsLost = true;
		}
		else if(gameIsWon){
			winMenu.SetActive(true);
			Time.timeScale = 0f;
			gameIsWon = true;
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(gameIsPaused && !gameIsWon && !gameIsLost){
				resume();
			}
			else{
				pause();
			}
		}
    }
}
