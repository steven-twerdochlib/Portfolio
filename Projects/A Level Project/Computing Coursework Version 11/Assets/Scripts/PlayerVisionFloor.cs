using UnityEngine;
using System.Collections;

public class PlayerVisionFloor : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		GetComponent<Renderer> ().material.color = Color.grey;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "PlayerVisionTag") 
		{
			GetComponent<Renderer> ().material.color = Color.black;
			transform.gameObject.tag = "VisibleFloor";
		}
	}
		
	void Update () {
	
	}
}
