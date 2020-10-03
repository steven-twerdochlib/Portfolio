using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IsScript : MonoBehaviour
{

	private GameObject leftNeighbour;
	private GameObject rightNeighbour;
	private GameObject aboveNeighbour;
	private GameObject belowNeighbour;
	public GameObject mainCamera;
	private bool gameObjectChanged = false;
	public int isTileNum;

	public void findNeighbourTiles(){
		Grid grid = transform.parent.GetComponent<Grid>();

		//Gets text tile left to the gamobject this script is attaced to.
		Vector3Int cellPosition = grid.WorldToCell(new Vector3((transform.position.x - grid.cellSize.x), transform.position.y, transform.position.z));
		Vector3 cellPositionCenter = grid.GetCellCenterWorld(cellPosition);
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("TextTile")){
			Vector3Int gameObjectCell = grid.WorldToCell(go.transform.position);
			if(grid.GetCellCenterWorld(gameObjectCell) == cellPositionCenter){
				leftNeighbour = go;
				gameObjectChanged = true;
			}
		}
		if(gameObjectChanged == false){
			leftNeighbour = null;
		}
		else{
			gameObjectChanged = false;
		}

		//Gets text tile right to the gamobject this script is attaced to.
		cellPosition = grid.WorldToCell(new Vector3((transform.position.x + grid.cellSize.x), transform.position.y, transform.position.z));
		cellPositionCenter = grid.GetCellCenterWorld(cellPosition);
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("TextTile")){
			Vector3Int gameObjectCell = grid.WorldToCell(go.transform.position);
			if(grid.GetCellCenterWorld(gameObjectCell) == cellPositionCenter){
				rightNeighbour = go;
				gameObjectChanged = true;
			}
		}
		if(gameObjectChanged == false){
			rightNeighbour = null;
		}
		else{
			gameObjectChanged = false;
		}
		//Gets text tile above to the gamobject this script is attaced to.
		cellPosition = grid.WorldToCell(new Vector3(transform.position.x, (transform.position.y + grid.cellSize.y), transform.position.z));
		cellPositionCenter = grid.GetCellCenterWorld(cellPosition);
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("TextTile")){
			Vector3Int gameObjectCell = grid.WorldToCell(go.transform.position);
			if(grid.GetCellCenterWorld(gameObjectCell) == cellPositionCenter){
				aboveNeighbour = go;
				gameObjectChanged = true;
			}
		}
		if(gameObjectChanged == false){
			aboveNeighbour = null;
		}
		else{
			gameObjectChanged = false;
		}

		//Gets text tile below to the gamobject this script is attaced to.
		cellPosition = grid.WorldToCell(new Vector3(transform.position.x, (transform.position.y - grid.cellSize.y), transform.position.z));
		cellPositionCenter = grid.GetCellCenterWorld(cellPosition);
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("TextTile")){
			Vector3Int gameObjectCell = grid.WorldToCell(go.transform.position);
			if(grid.GetCellCenterWorld(gameObjectCell) == cellPositionCenter){
				belowNeighbour = go;
				gameObjectChanged = true;
			}
		}
		if(gameObjectChanged == false){
			belowNeighbour = null;
		}
		else{
			gameObjectChanged = false;
		}
	}

	public void setRules(){
		ApplyRulesScript applyRulesScript = mainCamera.GetComponent<ApplyRulesScript>();
		applyRulesScript.isFlagYou[isTileNum] = false;
		applyRulesScript.isBabaYou[isTileNum] = false;
		applyRulesScript.isBoxYou[isTileNum] = false;
		applyRulesScript.isWallYou[isTileNum] = false;
		applyRulesScript.isWaterYou[isTileNum] = false;

		applyRulesScript.isFlagSink[isTileNum] = false;
		applyRulesScript.isBabaSink[isTileNum] = false;
		applyRulesScript.isBoxSink[isTileNum] = false;
		applyRulesScript.isWallSink[isTileNum] = false;
		applyRulesScript.isWaterSink[isTileNum] = false;

		applyRulesScript.isFlagFloat[isTileNum] = false;
		applyRulesScript.isBabaFloat[isTileNum] = false;
		applyRulesScript.isBoxFloat[isTileNum] = false;
		applyRulesScript.isWallFloat[isTileNum] = false;
		applyRulesScript.isWaterFloat[isTileNum] = false;

		applyRulesScript.isFlagWin[isTileNum] = false;
		applyRulesScript.isBabaWin[isTileNum] = false;
		applyRulesScript.isBoxWin[isTileNum] = false;
		applyRulesScript.isWallWin[isTileNum] = false;
		applyRulesScript.isWaterWin[isTileNum] = false;

		applyRulesScript.isFlagLose[isTileNum] = false;
		applyRulesScript.isBabaLose[isTileNum] = false;
		applyRulesScript.isBoxLose[isTileNum] = false;
		applyRulesScript.isWallLose[isTileNum] = false;
		applyRulesScript.isWaterLose[isTileNum] = false;

		//Checks You is rules
		if(aboveNeighbour != null && belowNeighbour != null && leftNeighbour != null && rightNeighbour != null){
			if((leftNeighbour.name.Contains("YouTile") && rightNeighbour.name.Contains("BoxTextTile")) || (leftNeighbour.name.Contains("BoxTextTile") && rightNeighbour.name.Contains("YouTile"))
				|| (aboveNeighbour.name.Contains("YouTile") && belowNeighbour.name.Contains("BoxTextTile")) || (aboveNeighbour.name.Contains("BoxTextTile") && belowNeighbour.name.Contains("YouTile"))){
				applyRulesScript.isBoxYou[isTileNum] = true;
			}
			else{
				applyRulesScript.isBoxYou[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("YouTile") && rightNeighbour.name.Contains("BabaTextTile")) || (leftNeighbour.name.Contains("BabaTextTile") && rightNeighbour.name.Contains("YouTile"))
				|| (aboveNeighbour.name.Contains("YouTile") && belowNeighbour.name.Contains("BabaTextTile")) || (aboveNeighbour.name.Contains("BabaTextTile") && belowNeighbour.name.Contains("YouTile"))){
				applyRulesScript.isBabaYou[isTileNum] = true;
			}
			else{
				applyRulesScript.isBabaYou[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("YouTile") && rightNeighbour.name.Contains("FlagTextTile")) || (leftNeighbour.name.Contains("FlagTextTile") && rightNeighbour.name.Contains("YouTile"))
				|| (aboveNeighbour.name.Contains("YouTile") && belowNeighbour.name.Contains("FlagTextTile")) || (aboveNeighbour.name.Contains("FlagTextTile") && belowNeighbour.name.Contains("YouTile"))){
				applyRulesScript.isFlagYou[isTileNum] = true;
			}
			else{
				applyRulesScript.isFlagYou[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("YouTile") && rightNeighbour.name.Contains("WaterTextTile")) || (leftNeighbour.name.Contains("WaterTextTile") && rightNeighbour.name.Contains("YouTile"))
				|| (aboveNeighbour.name.Contains("YouTile") && belowNeighbour.name.Contains("WaterTextTile")) || (aboveNeighbour.name.Contains("WaterTextTile") && belowNeighbour.name.Contains("YouTile"))){
				applyRulesScript.isWaterYou[isTileNum] = true;
			}
			else{
				applyRulesScript.isWaterYou[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("YouTile") && rightNeighbour.name.Contains("WallTextTile")) || (leftNeighbour.name.Contains("WallTextTile") && rightNeighbour.name.Contains("YouTile"))
				|| (aboveNeighbour.name.Contains("YouTile") && belowNeighbour.name.Contains("WallTextTile")) || (aboveNeighbour.name.Contains("WallTextTile") && belowNeighbour.name.Contains("YouTile"))){
				applyRulesScript.isWallYou[isTileNum] = true;
			}
			else{
				applyRulesScript.isWallYou[isTileNum] = false;
			}
			//Checks SinkIs rules
			if((leftNeighbour.name.Contains("SinkTextTile") && rightNeighbour.name.Contains("BoxTextTile")) || (leftNeighbour.name.Contains("BoxTextTile") && rightNeighbour.name.Contains("SinkTextTile"))
				|| (aboveNeighbour.name.Contains("SinkTextTile") && belowNeighbour.name.Contains("BoxTextTile")) || (aboveNeighbour.name.Contains("BoxTextTile") && belowNeighbour.name.Contains("SinkTextTile"))){
				applyRulesScript.isBoxSink[isTileNum] = true;
			}
			else{
				applyRulesScript.isBoxSink[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("SinkTextTile") && rightNeighbour.name.Contains("BabaTextTile")) || (leftNeighbour.name.Contains("BabaTextTile") && rightNeighbour.name.Contains("SinkTextTile"))
				|| (aboveNeighbour.name.Contains("SinkTextTile") && belowNeighbour.name.Contains("BabaTextTile")) || (aboveNeighbour.name.Contains("BabaTextTile") && belowNeighbour.name.Contains("SinkTextTile"))){
				applyRulesScript.isBabaSink[isTileNum] = true;
			}
			else{
				applyRulesScript.isBabaSink[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("SinkTextTile") && rightNeighbour.name.Contains("FlagTextTile")) || (leftNeighbour.name.Contains("FlagTextTile") && rightNeighbour.name.Contains("SinkTextTile"))
				|| (aboveNeighbour.name.Contains("SinkTextTile") && belowNeighbour.name.Contains("FlagTextTile")) || (aboveNeighbour.name.Contains("FlagTextTile") && belowNeighbour.name.Contains("SinkTextTile"))){
				applyRulesScript.isFlagSink[isTileNum] = true;
			}
			else{
				applyRulesScript.isFlagSink[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("SinkTextTile") && rightNeighbour.name.Contains("WaterTextTile")) || (leftNeighbour.name.Contains("WaterTextTile") && rightNeighbour.name.Contains("SinkTextTile"))
				|| (aboveNeighbour.name.Contains("SinkTextTile") && belowNeighbour.name.Contains("WaterTextTile")) || (aboveNeighbour.name.Contains("WaterTextTile") && belowNeighbour.name.Contains("SinkTextTile"))){
				applyRulesScript.isWaterSink[isTileNum] = true;
			}
			else{
				applyRulesScript.isWaterSink[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("SinkTextTile") && rightNeighbour.name.Contains("WallTextTile")) || (leftNeighbour.name.Contains("WallTextTile") && rightNeighbour.name.Contains("SinkTextTile"))
				|| (aboveNeighbour.name.Contains("SinkTextTile") && belowNeighbour.name.Contains("WallTextTile")) || (aboveNeighbour.name.Contains("WallTextTile") && belowNeighbour.name.Contains("SinkTextTile"))){
				applyRulesScript.isWallSink[isTileNum] = true;
			}
			else{
				applyRulesScript.isWallSink[isTileNum] = false;
			}
			//Checks FloatIs rules
			if((leftNeighbour.name.Contains("FloatTextTile") && rightNeighbour.name.Contains("BoxTextTile")) || (leftNeighbour.name.Contains("BoxTextTile") && rightNeighbour.name.Contains("FloatTextTile"))
				|| (aboveNeighbour.name.Contains("FloatTextTile") && belowNeighbour.name.Contains("BoxTextTile")) || (aboveNeighbour.name.Contains("BoxTextTile") && belowNeighbour.name.Contains("FloatTextTile"))){
				applyRulesScript.isBoxFloat[isTileNum] = true;
			}
			else{
				applyRulesScript.isBoxFloat[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("FloatTextTile") && rightNeighbour.name.Contains("BabaTextTile")) || (leftNeighbour.name.Contains("BabaTextTile") && rightNeighbour.name.Contains("FloatTextTile"))
				|| (aboveNeighbour.name.Contains("FloatTextTile") && belowNeighbour.name.Contains("BabaTextTile")) || (aboveNeighbour.name.Contains("BabaTextTile") && belowNeighbour.name.Contains("FloatTextTile"))){
				applyRulesScript.isBabaFloat[isTileNum] = true;
			}
			else{
				applyRulesScript.isBabaFloat[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("FloatTextTile") && rightNeighbour.name.Contains("FlagTextTile")) || (leftNeighbour.name.Contains("FlagTextTile") && rightNeighbour.name.Contains("FloatTextTile"))
				|| (aboveNeighbour.name.Contains("FloatTextTile") && belowNeighbour.name.Contains("FlagTextTile")) || (aboveNeighbour.name.Contains("FlagTextTile") && belowNeighbour.name.Contains("FloatTextTile"))){
				applyRulesScript.isFlagFloat[isTileNum] = true;
			}
			else{
				applyRulesScript.isFlagFloat[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("FloatTextTile") && rightNeighbour.name.Contains("WaterTextTile")) || (leftNeighbour.name.Contains("WaterTextTile") && rightNeighbour.name.Contains("FloatTextTile"))
				|| (aboveNeighbour.name.Contains("FloatTextTile") && belowNeighbour.name.Contains("WaterTextTile")) || (aboveNeighbour.name.Contains("WaterTextTile") && belowNeighbour.name.Contains("FloatTextTile"))){
				applyRulesScript.isWaterFloat[isTileNum] = true;
			}
			else{
				applyRulesScript.isWaterFloat[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("FloatTextTile") && rightNeighbour.name.Contains("WallTextTile")) || (leftNeighbour.name.Contains("WallTextTile") && rightNeighbour.name.Contains("FloatTextTile"))
				|| (aboveNeighbour.name.Contains("FloatTextTile") && belowNeighbour.name.Contains("WallTextTile")) || (aboveNeighbour.name.Contains("WallTextTile") && belowNeighbour.name.Contains("FloatTextTile"))){
				applyRulesScript.isWallFloat[isTileNum] = true;
			}
			else{
				applyRulesScript.isWallFloat[isTileNum] = false;
			}
			//Checks WinIs rule
			if((leftNeighbour.name.Contains("WinTextTile") && rightNeighbour.name.Contains("BoxTextTile")) || (leftNeighbour.name.Contains("BoxTextTile") && rightNeighbour.name.Contains("WinTextTile"))
				|| (aboveNeighbour.name.Contains("WinTextTile") && belowNeighbour.name.Contains("BoxTextTile")) || (aboveNeighbour.name.Contains("BoxTextTile") && belowNeighbour.name.Contains("WinTextTile"))){
				applyRulesScript.isBoxWin[isTileNum] = true;
			}
			else{
				applyRulesScript.isBoxWin[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("WinTextTile") && rightNeighbour.name.Contains("BabaTextTile")) || (leftNeighbour.name.Contains("BabaTextTile") && rightNeighbour.name.Contains("WinTextTile"))
				|| (aboveNeighbour.name.Contains("WinTextTile") && belowNeighbour.name.Contains("BabaTextTile")) || (aboveNeighbour.name.Contains("BabaTextTile") && belowNeighbour.name.Contains("WinTextTile"))){
				applyRulesScript.isBabaWin[isTileNum] = true;
			}
			else{
				applyRulesScript.isBabaWin[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("WinTextTile") && rightNeighbour.name.Contains("FlagTextTile")) || (leftNeighbour.name.Contains("FlagTextTile") && rightNeighbour.name.Contains("WinTextTile"))
				|| (aboveNeighbour.name.Contains("WinTextTile") && belowNeighbour.name.Contains("FlagTextTile")) || (aboveNeighbour.name.Contains("FlagTextTile") && belowNeighbour.name.Contains("WinTextTile"))){
				applyRulesScript.isFlagWin[isTileNum] = true;
			}
			else{
				applyRulesScript.isFlagWin[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("WinTextTile") && rightNeighbour.name.Contains("WaterTextTile")) || (leftNeighbour.name.Contains("WaterTextTile") && rightNeighbour.name.Contains("WinTextTile"))
				|| (aboveNeighbour.name.Contains("WinTextTile") && belowNeighbour.name.Contains("WaterTextTile")) || (aboveNeighbour.name.Contains("WaterTextTile") && belowNeighbour.name.Contains("WinTextTile"))){
				applyRulesScript.isWaterWin[isTileNum] = true;
			}
			else{
				applyRulesScript.isWaterWin[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("WinTextTile") && rightNeighbour.name.Contains("WallTextTile")) || (leftNeighbour.name.Contains("WallTextTile") && rightNeighbour.name.Contains("WinTextTile"))
				|| (aboveNeighbour.name.Contains("WinTextTile") && belowNeighbour.name.Contains("WallTextTile")) || (aboveNeighbour.name.Contains("WallTextTile") && belowNeighbour.name.Contains("WinTextTile"))){
				applyRulesScript.isWallWin[isTileNum] = true;
			}
			else{
				applyRulesScript.isWallWin[isTileNum] = false;
			}
			//Checks LoseIs rule
			if((leftNeighbour.name.Contains("LoseTextTile") && rightNeighbour.name.Contains("BoxTextTile")) || (leftNeighbour.name.Contains("BoxTextTile") && rightNeighbour.name.Contains("LoseTextTile"))
				|| (aboveNeighbour.name.Contains("LoseTextTile") && belowNeighbour.name.Contains("BoxTextTile")) || (aboveNeighbour.name.Contains("BoxTextTile") && belowNeighbour.name.Contains("LoseTextTile"))){
				applyRulesScript.isBoxLose[isTileNum] = true;
			}
			else{
				applyRulesScript.isBoxLose[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("LoseTextTile") && rightNeighbour.name.Contains("BabaTextTile")) || (leftNeighbour.name.Contains("BabaTextTile") && rightNeighbour.name.Contains("LoseTextTile"))
				|| (aboveNeighbour.name.Contains("LoseTextTile") && belowNeighbour.name.Contains("BabaTextTile")) || (aboveNeighbour.name.Contains("BabaTextTile") && belowNeighbour.name.Contains("LoseTextTile"))){
				applyRulesScript.isBabaLose[isTileNum] = true;
			}
			else{
				applyRulesScript.isBabaLose[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("LoseTextTile") && rightNeighbour.name.Contains("FlagTextTile")) || (leftNeighbour.name.Contains("FlagTextTile") && rightNeighbour.name.Contains("LoseTextTile"))
				|| (aboveNeighbour.name.Contains("LoseTextTile") && belowNeighbour.name.Contains("FlagTextTile")) || (aboveNeighbour.name.Contains("FlagTextTile") && belowNeighbour.name.Contains("LoseTextTile"))){
				applyRulesScript.isFlagLose[isTileNum] = true;
			}
			else{
				applyRulesScript.isFlagLose[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("LoseTextTile") && rightNeighbour.name.Contains("WaterTextTile")) || (leftNeighbour.name.Contains("WaterTextTile") && rightNeighbour.name.Contains("LoseTextTile"))
				|| (aboveNeighbour.name.Contains("LoseTextTile") && belowNeighbour.name.Contains("WaterTextTile")) || (aboveNeighbour.name.Contains("WaterTextTile") && belowNeighbour.name.Contains("LoseTextTile"))){
				applyRulesScript.isWaterLose[isTileNum] = true;
			}
			else{
				applyRulesScript.isWaterLose[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("LoseTextTile") && rightNeighbour.name.Contains("WallTextTile")) || (leftNeighbour.name.Contains("WallTextTile") && rightNeighbour.name.Contains("LoseTextTile"))
				|| (aboveNeighbour.name.Contains("LoseTextTile") && belowNeighbour.name.Contains("WallTextTile")) || (aboveNeighbour.name.Contains("WallTextTile") && belowNeighbour.name.Contains("LoseTextTile"))){
				applyRulesScript.isWallLose[isTileNum] = true;
			}
			else{
				applyRulesScript.isWallLose[isTileNum] = false;
			}
			//Checks PushIs rule
			if((leftNeighbour.name.Contains("PushTextTile") && rightNeighbour.name.Contains("BoxTextTile")) || (leftNeighbour.name.Contains("BoxTextTile") && rightNeighbour.name.Contains("PushTextTile"))
				|| (aboveNeighbour.name.Contains("PushTextTile") && belowNeighbour.name.Contains("BoxTextTile")) || (aboveNeighbour.name.Contains("BoxTextTile") && belowNeighbour.name.Contains("PushTextTile"))){
				applyRulesScript.isBoxPush[isTileNum] = true;
			}
			else{
				applyRulesScript.isBoxPush[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("PushTextTile") && rightNeighbour.name.Contains("BabaTextTile")) || (leftNeighbour.name.Contains("BabaTextTile") && rightNeighbour.name.Contains("PushTextTile"))
				|| (aboveNeighbour.name.Contains("PushTextTile") && belowNeighbour.name.Contains("BabaTextTile")) || (aboveNeighbour.name.Contains("BabaTextTile") && belowNeighbour.name.Contains("PushTextTile"))){
				applyRulesScript.isBabaPush[isTileNum] = true;
			}
			else{
				applyRulesScript.isBabaPush[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("PushTextTile") && rightNeighbour.name.Contains("FlagTextTile")) || (leftNeighbour.name.Contains("FlagTextTile") && rightNeighbour.name.Contains("PushTextTile"))
				|| (aboveNeighbour.name.Contains("PushTextTile") && belowNeighbour.name.Contains("FlagTextTile")) || (aboveNeighbour.name.Contains("FlagTextTile") && belowNeighbour.name.Contains("PushTextTile"))){
				applyRulesScript.isFlagPush[isTileNum] = true;
			}
			else{
				applyRulesScript.isFlagPush[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("PushTextTile") && rightNeighbour.name.Contains("WaterTextTile")) || (leftNeighbour.name.Contains("WaterTextTile") && rightNeighbour.name.Contains("PushTextTile"))
				|| (aboveNeighbour.name.Contains("PushTextTile") && belowNeighbour.name.Contains("WaterTextTile")) || (aboveNeighbour.name.Contains("WaterTextTile") && belowNeighbour.name.Contains("PushTextTile"))){
				applyRulesScript.isWaterPush[isTileNum] = true;
			}
			else{
				applyRulesScript.isWaterPush[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("PushTextTile") && rightNeighbour.name.Contains("WallTextTile")) || (leftNeighbour.name.Contains("WallTextTile") && rightNeighbour.name.Contains("PushTextTile"))
				|| (aboveNeighbour.name.Contains("PushTextTile") && belowNeighbour.name.Contains("WallTextTile")) || (aboveNeighbour.name.Contains("WallTextTile") && belowNeighbour.name.Contains("PushTextTile"))){
				applyRulesScript.isWallPush[isTileNum] = true;
			}
			else{
				applyRulesScript.isWallPush[isTileNum] = false;
			}
		}
		else if((leftNeighbour == null && aboveNeighbour == null) || (rightNeighbour == null && aboveNeighbour == null) 
			|| (leftNeighbour == null && belowNeighbour == null) || (rightNeighbour == null && belowNeighbour == null)){
			//Do nothing
		}
		else if(leftNeighbour == null || rightNeighbour == null){
			if((aboveNeighbour.name.Contains("YouTile") && belowNeighbour.name.Contains("BoxTextTile")) || (aboveNeighbour.name.Contains("BoxTextTile") && belowNeighbour.name.Contains("YouTile"))){
				applyRulesScript.isBoxYou[isTileNum] = true;
			}
			else{
				applyRulesScript.isBoxYou[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("YouTile") && belowNeighbour.name.Contains("BabaTextTile")) || (aboveNeighbour.name.Contains("BabaTextTile") && belowNeighbour.name.Contains("YouTile"))){
				applyRulesScript.isBabaYou[isTileNum] = true;
			}
			else{
				applyRulesScript.isBabaYou[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("YouTile") && belowNeighbour.name.Contains("FlagTextTile")) || (aboveNeighbour.name.Contains("FlagTextTile") && belowNeighbour.name.Contains("YouTile"))){
				applyRulesScript.isFlagYou[isTileNum] = true;
			}
			else{
				applyRulesScript.isFlagYou[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("YouTile") && belowNeighbour.name.Contains("WaterTextTile")) || (aboveNeighbour.name.Contains("WaterTextTile") && belowNeighbour.name.Contains("YouTile"))){
				applyRulesScript.isWaterYou[isTileNum] = true;
			}
			else{
				applyRulesScript.isWaterYou[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("YouTile") && belowNeighbour.name.Contains("WallTextTile")) || (aboveNeighbour.name.Contains("WallTextTile") && belowNeighbour.name.Contains("YouTile"))){
				applyRulesScript.isWallYou[isTileNum] = true;
			}
			else{
				applyRulesScript.isWallYou[isTileNum] = false;
			}
			//Checks SinkIs rules
			if((aboveNeighbour.name.Contains("SinkTextTile") && belowNeighbour.name.Contains("BoxTextTile")) || (aboveNeighbour.name.Contains("BoxTextTile") && belowNeighbour.name.Contains("SinkTextTile"))){
				applyRulesScript.isBoxSink[isTileNum] = true;
			}
			else{
				applyRulesScript.isBoxSink[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("SinkTextTile") && belowNeighbour.name.Contains("BabaTextTile")) || (aboveNeighbour.name.Contains("BabaTextTile") && belowNeighbour.name.Contains("SinkTextTile"))){
				applyRulesScript.isBabaSink[isTileNum] = true;
			}
			else{
				applyRulesScript.isBabaSink[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("SinkTextTile") && belowNeighbour.name.Contains("FlagTextTile")) || (aboveNeighbour.name.Contains("FlagTextTile") && belowNeighbour.name.Contains("SinkTextTile"))){
				applyRulesScript.isFlagSink[isTileNum] = true;
			}
			else{
				applyRulesScript.isFlagSink[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("SinkTextTile") && belowNeighbour.name.Contains("WaterTextTile")) || (aboveNeighbour.name.Contains("WaterTextTile") && belowNeighbour.name.Contains("SinkTextTile"))){
				applyRulesScript.isWaterSink[isTileNum] = true;
			}
			else{
				applyRulesScript.isWaterSink[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("SinkTextTile") && belowNeighbour.name.Contains("WallTextTile")) || (aboveNeighbour.name.Contains("WallTextTile") && belowNeighbour.name.Contains("SinkTextTile"))){
				applyRulesScript.isWallSink[isTileNum] = true;
			}
			else{
				applyRulesScript.isWallSink[isTileNum] = false;
			}
			//Checks FloatIs rules
			if((aboveNeighbour.name.Contains("FloatTextTile") && belowNeighbour.name.Contains("BoxTextTile")) || (aboveNeighbour.name.Contains("BoxTextTile") && belowNeighbour.name.Contains("FloatTextTile"))){
				applyRulesScript.isBoxFloat[isTileNum] = true;
			}
			else{
				applyRulesScript.isBoxFloat[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("FloatTextTile") && belowNeighbour.name.Contains("BabaTextTile")) || (aboveNeighbour.name.Contains("BabaTextTile") && belowNeighbour.name.Contains("FloatTextTile"))){
				applyRulesScript.isBabaFloat[isTileNum] = true;
			}
			else{
				applyRulesScript.isBabaFloat[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("FloatTextTile") && belowNeighbour.name.Contains("FlagTextTile")) || (aboveNeighbour.name.Contains("FlagTextTile") && belowNeighbour.name.Contains("FloatTextTile"))){
				applyRulesScript.isFlagFloat[isTileNum] = true;
			}
			else{
				applyRulesScript.isFlagFloat[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("FloatTextTile") && belowNeighbour.name.Contains("WaterTextTile")) || (aboveNeighbour.name.Contains("WaterTextTile") && belowNeighbour.name.Contains("FloatTextTile"))){
				applyRulesScript.isWaterFloat[isTileNum] = true;
			}
			else{
				applyRulesScript.isWaterFloat[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("FloatTextTile") && belowNeighbour.name.Contains("WallTextTile")) || (aboveNeighbour.name.Contains("WallTextTile") && belowNeighbour.name.Contains("FloatTextTile"))){
				applyRulesScript.isWallFloat[isTileNum] = true;
			}
			else{
				applyRulesScript.isWallFloat[isTileNum] = false;
			}
			//Checks WinIs rule
			if((aboveNeighbour.name.Contains("WinTextTile") && belowNeighbour.name.Contains("BoxTextTile")) || (aboveNeighbour.name.Contains("BoxTextTile") && belowNeighbour.name.Contains("WinTextTile"))){
				applyRulesScript.isBoxWin[isTileNum] = true;
			}
			else{
				applyRulesScript.isBoxWin[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("WinTextTile") && belowNeighbour.name.Contains("BabaTextTile")) || (aboveNeighbour.name.Contains("BabaTextTile") && belowNeighbour.name.Contains("WinTextTile"))){
				applyRulesScript.isBabaWin[isTileNum] = true;
			}
			else{
				applyRulesScript.isBabaWin[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("WinTextTile") && belowNeighbour.name.Contains("FlagTextTile")) || (aboveNeighbour.name.Contains("FlagTextTile") && belowNeighbour.name.Contains("WinTextTile"))){
				applyRulesScript.isFlagWin[isTileNum] = true;
			}
			else{
				applyRulesScript.isFlagWin[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("WinTextTile") && belowNeighbour.name.Contains("WaterTextTile")) || (aboveNeighbour.name.Contains("WaterTextTile") && belowNeighbour.name.Contains("WinTextTile"))){
				applyRulesScript.isWaterWin[isTileNum] = true;
			}
			else{
				applyRulesScript.isWaterWin[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("WinTextTile") && belowNeighbour.name.Contains("WallTextTile")) || (aboveNeighbour.name.Contains("WallTextTile") && belowNeighbour.name.Contains("WinTextTile"))){
				applyRulesScript.isWallWin[isTileNum] = true;
			}
			else{
				applyRulesScript.isWallWin[isTileNum] = false;
			}
			//Checks LoseIs rule
			if((aboveNeighbour.name.Contains("LoseTextTile") && belowNeighbour.name.Contains("BoxTextTile")) || (aboveNeighbour.name.Contains("BoxTextTile") && belowNeighbour.name.Contains("LoseTextTile"))){
				applyRulesScript.isBoxLose[isTileNum] = true;
			}
			else{
				applyRulesScript.isBoxLose[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("LoseTextTile") && belowNeighbour.name.Contains("BabaTextTile")) || (aboveNeighbour.name.Contains("BabaTextTile") && belowNeighbour.name.Contains("LoseTextTile"))){
				applyRulesScript.isBabaLose[isTileNum] = true;
			}
			else{
				applyRulesScript.isBabaLose[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("LoseTextTile") && belowNeighbour.name.Contains("FlagTextTile")) || (aboveNeighbour.name.Contains("FlagTextTile") && belowNeighbour.name.Contains("LoseTextTile"))){
				applyRulesScript.isFlagLose[isTileNum] = true;
			}
			else{
				applyRulesScript.isFlagLose[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("LoseTextTile") && belowNeighbour.name.Contains("WaterTextTile")) || (aboveNeighbour.name.Contains("WaterTextTile") && belowNeighbour.name.Contains("LoseTextTile"))){
				applyRulesScript.isWaterLose[isTileNum] = true;
			}
			else{
				applyRulesScript.isWaterLose[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("LoseTextTile") && belowNeighbour.name.Contains("WallTextTile")) || (aboveNeighbour.name.Contains("WallTextTile") && belowNeighbour.name.Contains("LoseTextTile"))){
				applyRulesScript.isWallLose[isTileNum] = true;
			}
			else{
				applyRulesScript.isWallLose[isTileNum] = false;
			}
			//Checks PushIs rule
			if((aboveNeighbour.name.Contains("PushTextTile") && belowNeighbour.name.Contains("BoxTextTile")) || (aboveNeighbour.name.Contains("BoxTextTile") && belowNeighbour.name.Contains("PushTextTile"))){
				applyRulesScript.isBoxPush[isTileNum] = true;
			}
			else{
				applyRulesScript.isBoxPush[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("PushTextTile") && belowNeighbour.name.Contains("BabaTextTile")) || (aboveNeighbour.name.Contains("BabaTextTile") && belowNeighbour.name.Contains("PushTextTile"))){
				applyRulesScript.isBabaPush[isTileNum] = true;
			}
			else{
				applyRulesScript.isBabaPush[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("PushTextTile") && belowNeighbour.name.Contains("FlagTextTile")) || (aboveNeighbour.name.Contains("FlagTextTile") && belowNeighbour.name.Contains("PushTextTile"))){
				applyRulesScript.isFlagPush[isTileNum] = true;
			}
			else{
				applyRulesScript.isFlagPush[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("PushTextTile") && belowNeighbour.name.Contains("WaterTextTile")) || (aboveNeighbour.name.Contains("WaterTextTile") && belowNeighbour.name.Contains("PushTextTile"))){
				applyRulesScript.isWaterPush[isTileNum] = true;
			}
			else{
				applyRulesScript.isWaterPush[isTileNum] = false;
			}
			if((aboveNeighbour.name.Contains("PushTextTile") && belowNeighbour.name.Contains("WallTextTile")) || (aboveNeighbour.name.Contains("WallTextTile") && belowNeighbour.name.Contains("PushTextTile"))){
				applyRulesScript.isWallPush[isTileNum] = true;
			}
			else{
				applyRulesScript.isWallPush[isTileNum] = false;
			}
		}
		else if(aboveNeighbour == null || belowNeighbour == null){
			if((leftNeighbour.name.Contains("YouTile") && rightNeighbour.name.Contains("BoxTextTile")) || (leftNeighbour.name.Contains("BoxTextTile") && rightNeighbour.name.Contains("YouTile"))){
				applyRulesScript.isBoxYou[isTileNum] = true;
			}
			else{
				applyRulesScript.isBoxYou[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("YouTile") && rightNeighbour.name.Contains("BabaTextTile")) || (leftNeighbour.name.Contains("BabaTextTile") && rightNeighbour.name.Contains("YouTile"))){
				applyRulesScript.isBabaYou[isTileNum] = true;
			}
			else{
				applyRulesScript.isBabaYou[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("YouTile") && rightNeighbour.name.Contains("FlagTextTile")) || (leftNeighbour.name.Contains("FlagTextTile") && rightNeighbour.name.Contains("YouTile"))){
				applyRulesScript.isFlagYou[isTileNum] = true;
			}
			else{
				applyRulesScript.isFlagYou[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("YouTile") && rightNeighbour.name.Contains("WaterTextTile")) || (leftNeighbour.name.Contains("WaterTextTile") && rightNeighbour.name.Contains("YouTile"))){
				applyRulesScript.isWaterYou[isTileNum] = true;
			}
			else{
				applyRulesScript.isWaterYou[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("YouTile") && rightNeighbour.name.Contains("WallTextTile")) || (leftNeighbour.name.Contains("WallTextTile") && rightNeighbour.name.Contains("YouTile"))){
				applyRulesScript.isWallYou[isTileNum] = true;
			}
			else{
				applyRulesScript.isWallYou[isTileNum] = false;
			}
			//Checks SinkIs rules
			if((leftNeighbour.name.Contains("SinkTextTile") && rightNeighbour.name.Contains("BoxTextTile")) || (leftNeighbour.name.Contains("BoxTextTile") && rightNeighbour.name.Contains("SinkTextTile"))){
				applyRulesScript.isBoxSink[isTileNum] = true;
			}
			else{
				applyRulesScript.isBoxSink[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("SinkTextTile") && rightNeighbour.name.Contains("BabaTextTile")) || (leftNeighbour.name.Contains("BabaTextTile") && rightNeighbour.name.Contains("SinkTextTile"))){
				applyRulesScript.isBabaSink[isTileNum] = true;
			}
			else{
				applyRulesScript.isBabaSink[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("SinkTextTile") && rightNeighbour.name.Contains("FlagTextTile")) || (leftNeighbour.name.Contains("FlagTextTile") && rightNeighbour.name.Contains("SinkTextTile"))){
				applyRulesScript.isFlagSink[isTileNum] = true;
			}
			else{
				applyRulesScript.isFlagSink[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("SinkTextTile") && rightNeighbour.name.Contains("WaterTextTile")) || (leftNeighbour.name.Contains("WaterTextTile") && rightNeighbour.name.Contains("SinkTextTile"))){
				applyRulesScript.isWaterSink[isTileNum] = true;
			}
			else{
				applyRulesScript.isWaterSink[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("SinkTextTile") && rightNeighbour.name.Contains("WallTextTile")) || (leftNeighbour.name.Contains("WallTextTile") && rightNeighbour.name.Contains("SinkTextTile"))){
				applyRulesScript.isWallSink[isTileNum] = true;
			}
			else{
				applyRulesScript.isWallSink[isTileNum] = false;
			}
			//Checks FloatIs rules
			if((leftNeighbour.name.Contains("FloatTextTile") && rightNeighbour.name.Contains("BoxTextTile")) || (leftNeighbour.name.Contains("BoxTextTile") && rightNeighbour.name.Contains("FloatTextTile"))){
				applyRulesScript.isBoxFloat[isTileNum] = true;
			}
			else{
				applyRulesScript.isBoxFloat[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("FloatTextTile") && rightNeighbour.name.Contains("BabaTextTile")) || (leftNeighbour.name.Contains("BabaTextTile") && rightNeighbour.name.Contains("FloatTextTile"))){
				applyRulesScript.isBabaFloat[isTileNum] = true;
			}
			else{
				applyRulesScript.isBabaFloat[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("FloatTextTile") && rightNeighbour.name.Contains("FlagTextTile")) || (leftNeighbour.name.Contains("FlagTextTile") && rightNeighbour.name.Contains("FloatTextTile"))){
				applyRulesScript.isFlagFloat[isTileNum] = true;
			}
			else{
				applyRulesScript.isFlagFloat[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("FloatTextTile") && rightNeighbour.name.Contains("WaterTextTile")) || (leftNeighbour.name.Contains("WaterTextTile") && rightNeighbour.name.Contains("FloatTextTile"))){
				applyRulesScript.isWaterFloat[isTileNum] = true;
			}
			else{
				applyRulesScript.isWaterFloat[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("FloatTextTile") && rightNeighbour.name.Contains("WallTextTile")) || (leftNeighbour.name.Contains("WallTextTile") && rightNeighbour.name.Contains("FloatTextTile"))){
				applyRulesScript.isWallFloat[isTileNum] = true;
			}
			else{
				applyRulesScript.isWallFloat[isTileNum] = false;
			}
			//Checks WinIs rule
			if((leftNeighbour.name.Contains("WinTextTile") && rightNeighbour.name.Contains("BoxTextTile")) || (leftNeighbour.name.Contains("BoxTextTile") && rightNeighbour.name.Contains("WinTextTile"))){
				applyRulesScript.isBoxWin[isTileNum] = true;
			}
			else{
				applyRulesScript.isBoxWin[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("WinTextTile") && rightNeighbour.name.Contains("BabaTextTile")) || (leftNeighbour.name.Contains("BabaTextTile") && rightNeighbour.name.Contains("WinTextTile"))){
				applyRulesScript.isBabaWin[isTileNum] = true;
			}
			else{
				applyRulesScript.isBabaWin[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("WinTextTile") && rightNeighbour.name.Contains("FlagTextTile")) || (leftNeighbour.name.Contains("FlagTextTile") && rightNeighbour.name.Contains("WinTextTile"))){
				applyRulesScript.isFlagWin[isTileNum] = true;
			}
			else{
				applyRulesScript.isFlagWin[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("WinTextTile") && rightNeighbour.name.Contains("WaterTextTile")) || (leftNeighbour.name.Contains("WaterTextTile") && rightNeighbour.name.Contains("WinTextTile"))){
				applyRulesScript.isWaterWin[isTileNum] = true;
			}
			else{
				applyRulesScript.isWaterWin[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("WinTextTile") && rightNeighbour.name.Contains("WallTextTile")) || (leftNeighbour.name.Contains("WallTextTile") && rightNeighbour.name.Contains("WinTextTile"))){
				applyRulesScript.isWallWin[isTileNum] = true;
			}
			else{
				applyRulesScript.isWallWin[isTileNum] = false;
			}
			//Checks LoseIs rule
			if((leftNeighbour.name.Contains("LoseTextTile") && rightNeighbour.name.Contains("BoxTextTile")) || (leftNeighbour.name.Contains("BoxTextTile") && rightNeighbour.name.Contains("LoseTextTile"))){
				applyRulesScript.isBoxLose[isTileNum] = true;
			}
			else{
				applyRulesScript.isBoxLose[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("LoseTextTile") && rightNeighbour.name.Contains("BabaTextTile")) || (leftNeighbour.name.Contains("BabaTextTile") && rightNeighbour.name.Contains("LoseTextTile"))){
				applyRulesScript.isBabaLose[isTileNum] = true;
			}
			else{
				applyRulesScript.isBabaLose[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("LoseTextTile") && rightNeighbour.name.Contains("FlagTextTile")) || (leftNeighbour.name.Contains("FlagTextTile") && rightNeighbour.name.Contains("LoseTextTile"))){
				applyRulesScript.isFlagLose[isTileNum] = true;
			}
			else{
				applyRulesScript.isFlagLose[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("LoseTextTile") && rightNeighbour.name.Contains("WaterTextTile")) || (leftNeighbour.name.Contains("WaterTextTile") && rightNeighbour.name.Contains("LoseTextTile"))){
				applyRulesScript.isWaterLose[isTileNum] = true;
			}
			else{
				applyRulesScript.isWaterLose[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("LoseTextTile") && rightNeighbour.name.Contains("WallTextTile")) || (leftNeighbour.name.Contains("WallTextTile") && rightNeighbour.name.Contains("LoseTextTile"))){
				applyRulesScript.isWallLose[isTileNum] = true;
			}
			else{
				applyRulesScript.isWallLose[isTileNum] = false;
			}
			//Checks PushIs rule
			if((leftNeighbour.name.Contains("PushTextTile") && rightNeighbour.name.Contains("BoxTextTile")) || (leftNeighbour.name.Contains("BoxTextTile") && rightNeighbour.name.Contains("PushTextTile"))){
				applyRulesScript.isBoxPush[isTileNum] = true;
			}
			else{
				applyRulesScript.isBoxPush[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("PushTextTile") && rightNeighbour.name.Contains("BabaTextTile")) || (leftNeighbour.name.Contains("BabaTextTile") && rightNeighbour.name.Contains("PushTextTile"))){
				applyRulesScript.isBabaPush[isTileNum] = true;
			}
			else{
				applyRulesScript.isBabaPush[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("PushTextTile") && rightNeighbour.name.Contains("FlagTextTile")) || (leftNeighbour.name.Contains("FlagTextTile") && rightNeighbour.name.Contains("PushTextTile"))){
				applyRulesScript.isFlagPush[isTileNum] = true;
			}
			else{
				applyRulesScript.isFlagPush[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("PushTextTile") && rightNeighbour.name.Contains("WaterTextTile")) || (leftNeighbour.name.Contains("WaterTextTile") && rightNeighbour.name.Contains("PushTextTile"))){
				applyRulesScript.isWaterPush[isTileNum] = true;
			}
			else{
				applyRulesScript.isWaterPush[isTileNum] = false;
			}
			if((leftNeighbour.name.Contains("PushTextTile") && rightNeighbour.name.Contains("WallTextTile")) || (leftNeighbour.name.Contains("WallTextTile") && rightNeighbour.name.Contains("PushTextTile"))){
				applyRulesScript.isWallPush[isTileNum] = true;
			}
			else{
				applyRulesScript.isWallPush[isTileNum] = false;
			}
		}
		/*
		if((leftNeighbour.name.Contains("YouTile") && rightNeighbour.name.Contains("BoxTextTile")) || (leftNeighbour.name.Contains("BoxTextTile") && rightNeighbour.name.Contains("YouTile"))){
			if(GameObject.Find("BoxTile").GetComponent<CharacterControllerScript>() == null){
				GameObject.Find("BoxTile").AddComponent<CharacterControllerScript>();
			}
		}
		if((leftNeighbour.name.Contains("YouTile") && rightNeighbour.name.Contains("BabaTextTile")) || (leftNeighbour.name.Contains("BabaTextTile") && rightNeighbour.name.Contains("YouTile"))){
			if(GameObject.Find("CharacterForward").GetComponent<CharacterControllerScript>() == null){
				GameObject.Find("CharacterForward").AddComponent<CharacterControllerScript>();
			}
		}
		if((leftNeighbour.name.Contains("YouTile") && rightNeighbour.name.Contains("FlagTextTile")) || (leftNeighbour.name.Contains("FlagTextTile") && rightNeighbour.name.Contains("YouTile"))){
			if(GameObject.Find("FlagTile").GetComponent<CharacterControllerScript>() == null){
				GameObject.Find("FlagTile").AddComponent<CharacterControllerScript>();
			}
		}
		*/

	}

    // Update is called once per frame
    void Update()
	{
		findNeighbourTiles();
		setRules();

		/*
		if(leftNeighbour != null && rightNeighbour != null){
			changeRules(leftNeighbour, rightNeighbour);
		}
		if(aboveNeighbour != null && belowNeighbour != null){
			changeRules(aboveNeighbour, belowNeighbour);
		}

		try{
			Debug.Log(leftNeighbour.name);
		}
		catch(NullReferenceException e){}
		try{
			Debug.Log(rightNeighbour.name);
		}
		catch(NullReferenceException e){}
		try{
			Debug.Log(aboveNeighbour.name);
		}
		catch(NullReferenceException e){}
		try{
			Debug.Log(belowNeighbour.name);
		}
		catch(NullReferenceException e){}
		*/
	}
}
