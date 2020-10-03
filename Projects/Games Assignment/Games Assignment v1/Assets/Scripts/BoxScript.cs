using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoxScript : MonoBehaviour
{
	public bool canSink = false;
	public bool isPushable = false;
	public bool isWin = false;
	public bool isLose = false;
	public float moveSpeed = 0f;
	public Vector2 targetVelocity = new Vector2(0, 0);

	void Start(){
		GetComponent<Rigidbody2D>().velocity = targetVelocity;
		if(this.gameObject.tag == "TextTile" || this.gameObject.tag == "IsTextTile"){
			isPushable = true;
			canSink = false;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		bool objectDestroyed = false;
		if(col.gameObject.GetComponent<CharacterControllerScript>() != null){
			if(col.gameObject.GetComponent<CharacterControllerScript>().canSink){
				Destroy(gameObject);
				objectDestroyed = true;
			}
		}
		else if(col.gameObject.GetComponent<BoxScript>() != null){
			if(col.gameObject.GetComponent<BoxScript>().canSink){
				Destroy(gameObject);
				objectDestroyed = true;
			}
		}
		if(col.gameObject.tag == "Water" && !isWin && !isLose && GameObject.Find("Main Camera").GetComponent<ApplyRulesScript>().checkOneTrue(GameObject.Find("Main Camera").GetComponent<ApplyRulesScript>().isWaterSink)){
			Destroy(gameObject);
			objectDestroyed = true;
		}
		else if(col.gameObject.tag == "Water" && !GameObject.Find("Main Camera").GetComponent<ApplyRulesScript>().checkOneTrue(GameObject.Find("Main Camera").GetComponent<ApplyRulesScript>().isWaterSink)){
			col.gameObject.GetComponent<TilemapCollider2D>().isTrigger = true;
		}
		if(!objectDestroyed){
			if(col.gameObject.tag != "Wall" && col.gameObject.tag != "Water" && isPushable == true){
				GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
				if(col.gameObject.GetComponent<CharacterControllerScript>() != null){
					targetVelocity = col.gameObject.GetComponent<CharacterControllerScript>().targetVelocity * col.gameObject.GetComponent<CharacterControllerScript>().moveSpeed;
					moveSpeed = col.gameObject.GetComponent<CharacterControllerScript>().moveSpeed;
					GetComponent<Rigidbody2D>().velocity = targetVelocity;
				}
				else if(col.gameObject.GetComponent<BoxScript>() != null){
					GetComponent<Rigidbody2D>().velocity = col.gameObject.GetComponent<BoxScript>().targetVelocity * col.gameObject.GetComponent<BoxScript>().moveSpeed;
					GetComponent<Rigidbody2D>().velocity = targetVelocity;
					moveSpeed = col.gameObject.GetComponent<BoxScript>().moveSpeed;
				}
			}
		}
	}

	void Update(){	//Problem with this function as any objects still inside the water tile when the rule Water is Sink is true again aren't destroyed.
		if(GameObject.Find("Main Camera").GetComponent<ApplyRulesScript>().checkOneTrue(GameObject.Find("Main Camera").GetComponent<ApplyRulesScript>().isWaterSink)){
			GameObject.Find("WaterTilemap").gameObject.GetComponent<TilemapCollider2D>().isTrigger = false;
		}
		else{
			GameObject.Find("WaterTilemap").gameObject.GetComponent<TilemapCollider2D>().isTrigger = true;
		}
	}

	void OnCollisionExit2D(Collision2D col){
		if(col.gameObject.tag != "Wall" && col.gameObject.tag != "Water" && isPushable == true){
			GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
			Grid grid = transform.parent.GetComponent<Grid>();
			Vector3Int cellPosition = grid.WorldToCell(transform.position);
			transform.position = grid.GetCellCenterWorld(cellPosition);
			targetVelocity = new Vector2(0, 0);
			GetComponent<Rigidbody2D>().velocity=targetVelocity;
			moveSpeed = 0f;
		}
	}
}
