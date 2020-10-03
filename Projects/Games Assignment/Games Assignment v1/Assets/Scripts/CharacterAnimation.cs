using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{

	public Animator animator;

    // Update is called once per frame
    void Update()
    {
		foreach (GameObject playerGO in GameObject.FindGameObjectsWithTag("Player")){
			if(playerGO.GetComponent<CharacterControllerScript>() != null){
				if(Input.GetKey("w")) {
					animator.SetBool("notMovingLeftRight", true);
					animator.SetBool("movingLeft", false);
					animator.SetBool("movingRight", false);
				}

				if(Input.GetKey("d")) {
					animator.SetBool("notMovingLeftRight", false);
					animator.SetBool("movingLeft", false);
					animator.SetBool("movingRight", true);
				}

				if(Input.GetKey("s")) {
					animator.SetBool("notMovingLeftRight", true);
					animator.SetBool("movingLeft", false);
					animator.SetBool("movingRight", false);
				}

				if(Input.GetKey("a")) {
					animator.SetBool("notMovingLeftRight", false);
					animator.SetBool("movingLeft", true);
					animator.SetBool("movingRight", false);
				}
				if(!Input.GetKey("a") && !Input.GetKey("s") && !Input.GetKey("d") && !Input.GetKey("w")){
					animator.SetBool("notMovingLeftRight", true);
					animator.SetBool("movingLeft", false);
					animator.SetBool("movingRight", false);
				}
			}
			else{
				animator.SetBool("notMovingLeftRight", true);
				animator.SetBool("movingLeft", false);
				animator.SetBool("movingRight", false);
			}
		}
    }
}
