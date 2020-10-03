using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour		//Has to be attached to Main Camera GameObject
{
	public void goToInstructionScene(){
		SceneManager.LoadScene("InstructionsScene");
	}

	public void goToLevelOneScene(){
		SceneManager.LoadScene("LevelOneScene");
	}

	public void goToLevelTwoScene(){
		SceneManager.LoadScene("LevelTwoScene");
	}

	public void goToMainMenuScene(){
		SceneManager.LoadScene("MainMenuScene");
	}

	public void goToLevelSelectScene(){
		SceneManager.LoadScene("LevelSelectScene");
	}
}
