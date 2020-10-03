using UnityEngine;
using System.Collections;

public class EnemyVision : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		GetComponent<Renderer> ().material.color = Color.grey;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "VisibleFloor")
		{
			GetComponent<Renderer> ().material.color = Color.red;
		}
		if (col.gameObject.tag == "PlayerVisionTag")
		{
			GetComponent<Renderer> ().material.color = Color.red;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
