using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyRulesScript : MonoBehaviour
{

	public bool[] isFlagYou;
	public bool[] isBabaYou;
	public bool[] isBoxYou;
	public bool[] isWallYou;
	public bool[] isWaterYou;

	public bool[] isFlagSink;
	public bool[] isBabaSink;
	public bool[] isBoxSink;
	public bool[] isWallSink;
	public bool[] isWaterSink;

	public bool[] isFlagFloat;
	public bool[] isBabaFloat;
	public bool[] isBoxFloat;
	public bool[] isWallFloat;
	public bool[] isWaterFloat;

	public bool[] isFlagWin;
	public bool[] isBabaWin;
	public bool[] isBoxWin;
	public bool[] isWallWin;
	public bool[] isWaterWin;

	public bool[] isFlagLose;
	public bool[] isBabaLose;
	public bool[] isBoxLose;
	public bool[] isWallLose;
	public bool[] isWaterLose;

	public bool[] isFlagPush;
	public bool[] isBabaPush;
	public bool[] isBoxPush;
	public bool[] isWallPush;
	public bool[] isWaterPush;

	public int numOfIsTiles = 0;

	void Start(){
		foreach (GameObject flagGO in GameObject.FindGameObjectsWithTag("IsTextTile")){
			numOfIsTiles++;
		}
		isFlagYou = new bool[numOfIsTiles];
		isBabaYou = new bool[numOfIsTiles];
		isBoxYou = new bool[numOfIsTiles];
		isWallYou = new bool[numOfIsTiles];
		isWaterYou = new bool[numOfIsTiles];

		isFlagSink = new bool[numOfIsTiles];
		isBabaSink = new bool[numOfIsTiles];
		isBoxSink = new bool[numOfIsTiles];
		isWallSink = new bool[numOfIsTiles];
		isWaterSink = new bool[numOfIsTiles];

		isFlagFloat = new bool[numOfIsTiles];
		isBabaFloat = new bool[numOfIsTiles];
		isBoxFloat = new bool[numOfIsTiles];
		isWallFloat = new bool[numOfIsTiles];
		isWaterFloat = new bool[numOfIsTiles];

		isFlagWin = new bool[numOfIsTiles];
		isBabaWin = new bool[numOfIsTiles];
		isBoxWin = new bool[numOfIsTiles];
		isWallWin = new bool[numOfIsTiles];
		isWaterWin = new bool[numOfIsTiles];

		isFlagLose = new bool[numOfIsTiles];
		isBabaLose = new bool[numOfIsTiles];
		isBoxLose = new bool[numOfIsTiles];
		isWallLose = new bool[numOfIsTiles];
		isWaterLose = new bool[numOfIsTiles];

		isFlagPush = new bool[numOfIsTiles];
		isBabaPush = new bool[numOfIsTiles];
		isBoxPush = new bool[numOfIsTiles];
		isWallPush = new bool[numOfIsTiles];
		isWaterPush = new bool[numOfIsTiles];
	}
	//checkOneTrue function returns true if there is at least one true in the given list or false if there are only falses in the list.
	public bool checkOneTrue(bool[] list){
		for(int x = 0; x < numOfIsTiles; x++){
			if(list[x] == true){
				return true;
			}
		}
		return false;
	}

    // Update is called once per frame
    void Update()
    {
		foreach (GameObject flagGO in GameObject.FindGameObjectsWithTag("Flag")){
			if(checkOneTrue(isFlagYou)){
				if(flagGO.GetComponent<BoxScript>() != null){
					Destroy(flagGO.GetComponent<BoxScript>());
				}
				if(flagGO.GetComponent<CharacterControllerScript>() == null){
					flagGO.AddComponent<CharacterControllerScript>();
				}
			}
			else{
				if(flagGO.GetComponent<CharacterControllerScript>() != null){
					Destroy(flagGO.GetComponent<CharacterControllerScript>());
				}
				if(flagGO.GetComponent<BoxScript>() == null){
					flagGO.AddComponent<BoxScript>();
				}
			}
			if(checkOneTrue(isFlagSink)){
				if(flagGO.GetComponent<BoxScript>() != null){
					flagGO.GetComponent<BoxScript>().canSink = true;
				}
				if(flagGO.GetComponent<CharacterControllerScript>() != null){
					flagGO.GetComponent<CharacterControllerScript>().canSink = true;
				}
			}
			else{
				if(flagGO.GetComponent<BoxScript>() != null){
					flagGO.GetComponent<BoxScript>().canSink = false;
				}
				if(flagGO.GetComponent<CharacterControllerScript>() != null){
					flagGO.GetComponent<CharacterControllerScript>().canSink = false;
				}
			}
			if(checkOneTrue(isFlagWin)){
				if(flagGO.GetComponent<BoxScript>() != null){
					flagGO.GetComponent<BoxScript>().isWin = true;
				}
				if(flagGO.GetComponent<CharacterControllerScript>() != null){
					flagGO.GetComponent<CharacterControllerScript>().isWin = true;
				}
			}
			else{
				if(flagGO.GetComponent<BoxScript>() != null){
					flagGO.GetComponent<BoxScript>().isWin = false;
				}
				if(flagGO.GetComponent<CharacterControllerScript>() != null){
					flagGO.GetComponent<CharacterControllerScript>().isWin = false;
				}
			}
			if(checkOneTrue(isFlagLose)){
				if(flagGO.GetComponent<BoxScript>() != null){
					flagGO.GetComponent<BoxScript>().isLose = true;
				}
				if(flagGO.GetComponent<CharacterControllerScript>() != null){
					flagGO.GetComponent<CharacterControllerScript>().isLose = true;
				}
			}
			else{
				if(flagGO.GetComponent<BoxScript>() != null){
					flagGO.GetComponent<BoxScript>().isLose = false;
				}
				if(flagGO.GetComponent<CharacterControllerScript>() != null){
					flagGO.GetComponent<CharacterControllerScript>().isLose = false;
				}
			}
			if(checkOneTrue(isFlagPush)){
				if(flagGO.GetComponent<BoxScript>() != null){
					flagGO.GetComponent<BoxScript>().isPushable = true;
				}
			}
			else{
				if(flagGO.GetComponent<BoxScript>() != null){
					flagGO.GetComponent<BoxScript>().isPushable = false;
				}
			}
		}
		foreach (GameObject boxGO in GameObject.FindGameObjectsWithTag("Box")){
			if(checkOneTrue(isBoxYou)){
				if(boxGO.GetComponent<BoxScript>() != null){
					Destroy(boxGO.GetComponent<BoxScript>());
				}
				if(boxGO.GetComponent<CharacterControllerScript>() == null){
					boxGO.AddComponent<CharacterControllerScript>();
				}
			}
			else{
				if(boxGO.GetComponent<CharacterControllerScript>() != null){
					Destroy(boxGO.GetComponent<CharacterControllerScript>());
				}
				if(boxGO.GetComponent<BoxScript>() == null){
					boxGO.AddComponent<BoxScript>();
				}
			}
			if(checkOneTrue(isBoxSink)){
				if(boxGO.GetComponent<BoxScript>() != null){
					boxGO.GetComponent<BoxScript>().canSink = true;
				}
				if(boxGO.GetComponent<CharacterControllerScript>() != null){
					boxGO.GetComponent<CharacterControllerScript>().canSink = true;
				}
			}
			else{
				if(boxGO.GetComponent<BoxScript>() != null){
					boxGO.GetComponent<BoxScript>().canSink = false;
				}
				if(boxGO.GetComponent<CharacterControllerScript>() != null){
					boxGO.GetComponent<CharacterControllerScript>().canSink = false;
				}
			}
			if(checkOneTrue(isBoxWin)){
				if(boxGO.GetComponent<BoxScript>() != null){
					boxGO.GetComponent<BoxScript>().isWin = true;
				}
				if(boxGO.GetComponent<CharacterControllerScript>() != null){
					boxGO.GetComponent<CharacterControllerScript>().isWin = true;
				}
			}
			else{
				if(boxGO.GetComponent<BoxScript>() != null){
					boxGO.GetComponent<BoxScript>().isWin = false;
				}
				if(boxGO.GetComponent<CharacterControllerScript>() != null){
					boxGO.GetComponent<CharacterControllerScript>().isWin = false;
				}
			}
			if(checkOneTrue(isBoxLose)){
				if(boxGO.GetComponent<BoxScript>() != null){
					boxGO.GetComponent<BoxScript>().isLose = true;
				}
				if(boxGO.GetComponent<CharacterControllerScript>() != null){
					boxGO.GetComponent<CharacterControllerScript>().isLose = true;
				}
			}
			else{
				if(boxGO.GetComponent<BoxScript>() != null){
					boxGO.GetComponent<BoxScript>().isLose = false;
				}
				if(boxGO.GetComponent<CharacterControllerScript>() != null){
					boxGO.GetComponent<CharacterControllerScript>().isLose = false;
				}
			}
			if(checkOneTrue(isBoxLose)){
				if(boxGO.GetComponent<BoxScript>() != null){
					boxGO.GetComponent<BoxScript>().isLose = true;
				}
				if(boxGO.GetComponent<CharacterControllerScript>() != null){
					boxGO.GetComponent<CharacterControllerScript>().isLose = true;
				}
			}
			else{
				if(boxGO.GetComponent<BoxScript>() != null){
					boxGO.GetComponent<BoxScript>().isLose = false;
				}
				if(boxGO.GetComponent<CharacterControllerScript>() != null){
					boxGO.GetComponent<CharacterControllerScript>().isLose = false;
				}
			}
			if(checkOneTrue(isBoxPush)){
				if(boxGO.GetComponent<BoxScript>() != null){
					boxGO.GetComponent<BoxScript>().isPushable = true;
				}
			}
			else{
				if(boxGO.GetComponent<BoxScript>() != null){
					boxGO.GetComponent<BoxScript>().isPushable = false;
				}
			}
		}
		foreach (GameObject playerGO in GameObject.FindGameObjectsWithTag("Player")){
			if(checkOneTrue(isBabaYou)){
				if(playerGO.GetComponent<BoxScript>() != null){
					Destroy(playerGO.GetComponent<BoxScript>());
				}
				if(playerGO.GetComponent<CharacterControllerScript>() == null){
					playerGO.AddComponent<CharacterControllerScript>();
				}
			}
			else{
				if(playerGO.GetComponent<CharacterControllerScript>() != null){
					Destroy(playerGO.GetComponent<CharacterControllerScript>());
				}
				if(playerGO.GetComponent<BoxScript>() == null){
					playerGO.AddComponent<BoxScript>();
				}
			}
			if(checkOneTrue(isBabaSink)){
				if(playerGO.GetComponent<BoxScript>() != null){
					playerGO.GetComponent<BoxScript>().canSink = true;
				}
				if(playerGO.GetComponent<CharacterControllerScript>() != null){
					playerGO.GetComponent<CharacterControllerScript>().canSink = true;
				}
			}
			else{
				if(playerGO.GetComponent<BoxScript>() != null){
					playerGO.GetComponent<BoxScript>().canSink = false;
				}
				if(playerGO.GetComponent<CharacterControllerScript>() != null){
					playerGO.GetComponent<CharacterControllerScript>().canSink = false;
				}
			}
			if(checkOneTrue(isBabaWin)){
				if(playerGO.GetComponent<BoxScript>() != null){
					playerGO.GetComponent<BoxScript>().isWin = true;
				}
				if(playerGO.GetComponent<CharacterControllerScript>() != null){
					playerGO.GetComponent<CharacterControllerScript>().isWin = true;
				}
			}
			else{
				if(playerGO.GetComponent<BoxScript>() != null){
					playerGO.GetComponent<BoxScript>().isWin = false;
				}
				if(playerGO.GetComponent<CharacterControllerScript>() != null){
					playerGO.GetComponent<CharacterControllerScript>().isWin = false;
				}
			}
			if(checkOneTrue(isBabaLose)){
				if(playerGO.GetComponent<BoxScript>() != null){
					playerGO.GetComponent<BoxScript>().isLose = true;
				}
				if(playerGO.GetComponent<CharacterControllerScript>() != null){
					playerGO.GetComponent<CharacterControllerScript>().isLose = true;
				}
			}
			else{
				if(playerGO.GetComponent<BoxScript>() != null){
					playerGO.GetComponent<BoxScript>().isLose = false;
				}
				if(playerGO.GetComponent<CharacterControllerScript>() != null){
					playerGO.GetComponent<CharacterControllerScript>().isLose = false;
				}
			}
			if(checkOneTrue(isBabaLose)){
				if(playerGO.GetComponent<BoxScript>() != null){
					playerGO.GetComponent<BoxScript>().isLose = true;
				}
				if(playerGO.GetComponent<CharacterControllerScript>() != null){
					playerGO.GetComponent<CharacterControllerScript>().isLose = true;
				}
			}
			else{
				if(playerGO.GetComponent<BoxScript>() != null){
					playerGO.GetComponent<BoxScript>().isLose = false;
				}
				if(playerGO.GetComponent<CharacterControllerScript>() != null){
					playerGO.GetComponent<CharacterControllerScript>().isLose = false;
				}
			}
			if(checkOneTrue(isBabaPush)){
				if(playerGO.GetComponent<BoxScript>() != null){
					playerGO.GetComponent<BoxScript>().isPushable = true;
				}
			}
			else{
				if(playerGO.GetComponent<BoxScript>() != null){
					playerGO.GetComponent<BoxScript>().isPushable = false;
				}
			}
		}
    }
}
