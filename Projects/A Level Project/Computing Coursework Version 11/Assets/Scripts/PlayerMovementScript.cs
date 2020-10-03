using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour {

    void Start()
    {
		
    }

    void FixedUpdate()
    {
        GameObject.FindGameObjectWithTag("PlayerVisionTag").transform.position = new Vector2(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y);
    }
}
