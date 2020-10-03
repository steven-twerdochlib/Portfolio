using UnityEngine;
using System.Collections;

public class PlayerVisionWall : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		GetComponent<Renderer> ().material.color = Color.grey;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "PlayerVisionTag")
		{
			GetComponent<Renderer> ().material.color = new Color32(255,255,255,255);
			transform.gameObject.tag = "VisibleWall";
		}
	}
}
