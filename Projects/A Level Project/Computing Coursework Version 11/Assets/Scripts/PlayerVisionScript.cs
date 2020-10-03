using UnityEngine;
using System.Collections;

public class PlayerVisionScript : MonoBehaviour {

    public GameObject PlayerVision;
    public GameObject Wall;
    public GameObject Floor;

    void Start ()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hello");
        if (collision.gameObject.tag == "PlayerVisionTag")
        {
            gameObject.GetComponent<Renderer>().material.color = Color.black;
        }
    }
    // Update is called once per frame
    void Update () {
	        
	}
}
