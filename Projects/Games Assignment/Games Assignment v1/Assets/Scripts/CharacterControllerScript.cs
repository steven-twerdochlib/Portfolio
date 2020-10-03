using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent (typeof (Rigidbody2D))]
public class CharacterControllerScript : MonoBehaviour
{
	public float moveSpeed = 5f;
	public bool canSink = false;
	public bool isWin = false;
	public bool isLose = false;
	public Vector2 targetVelocity = new Vector2(0, 0);
	public Tilemap waterTilemap;

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.GetComponent<CharacterControllerScript>() != null){
			if(col.gameObject.GetComponent<CharacterControllerScript>().canSink){
				Destroy(gameObject);
				return;
			}
		}
		else if(col.gameObject.GetComponent<BoxScript>() != null){
			if(col.gameObject.GetComponent<BoxScript>().canSink){
				Destroy(gameObject);
				return;
			}
		}
		if(col.gameObject.GetComponent<BoxScript>() != null){
			if(col.gameObject.GetComponent<BoxScript>().isWin){
				GameObject.Find("Main Camera").GetComponent<LevelMenus>().gameIsWon = true;
			}
			if(col.gameObject.GetComponent<BoxScript>().isLose){
				GameObject.Find("Main Camera").GetComponent<LevelMenus>().gameIsLost = true;
			}
		}
		if(col.gameObject.GetComponent<CharacterControllerScript>()){
			if(col.gameObject.GetComponent<CharacterControllerScript>().isWin){
				GameObject.Find("Main Camera").GetComponent<LevelMenus>().gameIsWon = true;
			}
			if(col.gameObject.GetComponent<CharacterControllerScript>().isLose){
				GameObject.Find("Main Camera").GetComponent<LevelMenus>().gameIsLost = true;
			}
		}
		if(col.gameObject.tag == "Water" && !isWin && !isLose && GameObject.Find("Main Camera").GetComponent<ApplyRulesScript>().checkOneTrue(GameObject.Find("Main Camera").GetComponent<ApplyRulesScript>().isWaterSink)){
			Destroy(gameObject);
			return;
		}
		else if(col.gameObject.tag == "Water" && !GameObject.Find("Main Camera").GetComponent<ApplyRulesScript>().checkOneTrue(GameObject.Find("Main Camera").GetComponent<ApplyRulesScript>().isWaterSink)){
			col.gameObject.GetComponent<TilemapCollider2D>().isTrigger = true;
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

    void FixedUpdate()
    {
		if(Input.GetKey("w")) {
			targetVelocity = new Vector2(0, Input.GetAxisRaw("Vertical"));
			GetComponent<Rigidbody2D>().velocity=targetVelocity * moveSpeed;
			return;
		}

		if(Input.GetKey("d")) {
			targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
			GetComponent<Rigidbody2D>().velocity=targetVelocity * moveSpeed;
			return;
		}

		if(Input.GetKey("s")) {
			targetVelocity = new Vector2(0, Input.GetAxisRaw("Vertical"));
			GetComponent<Rigidbody2D>().velocity=targetVelocity * moveSpeed;
			return;
		}

		if(Input.GetKey("a")) {
			targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
			GetComponent<Rigidbody2D>().velocity=targetVelocity * moveSpeed;
			return;
		}
		if(!Input.GetKey("a") && !Input.GetKey("s") && !Input.GetKey("d") && !Input.GetKey("w")){
			targetVelocity = new Vector2(0, 0);
			GetComponent<Rigidbody2D>().velocity=targetVelocity * moveSpeed;
			Grid grid = transform.parent.GetComponent<Grid>();
			Vector3Int cellPosition = grid.WorldToCell(transform.position);
			transform.position = grid.GetCellCenterWorld(cellPosition);
			return;
		}

    }
}
