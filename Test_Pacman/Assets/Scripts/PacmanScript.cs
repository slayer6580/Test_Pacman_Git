using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanScript : MonoBehaviour
{
	//private
	private float moveX;
	private float moveY;

	[SerializeField] 
	private float speed;
	private Vector3 direction;
	private CharacterController controller;
	private float timeSuperDot;

 	//public 
	public GameObject gameManager;
	public bool superDotMode;

    void Start()
    {
        controller = GetComponent<CharacterController>();
		timeSuperDot = gameManager.GetComponent<GameManagerScript>().timeSuperDot;
    }

    void Update()
    {
        //Input for mouvement
    	float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        direction = (transform.right * moveX + transform.forward * moveY);

        controller.Move(direction * speed * Time.deltaTime);
    }

	/*
	When colliding with Ghost of dots
	*/
    private void OnTriggerEnter(Collider collider)
    {
    	if(collider.gameObject.tag == "Ghost"){
			//if the superdot mode is activated, ghost go back to spawn area, else: pacman die and its game over
			if(superDotMode == true)
			{
				collider.gameObject.GetComponent<GhostScript>().behaviour = GhostScript.state.respawn;
			}
			else
			{
				gameManager.GetComponent<GameManagerScript>().EndGame();
			}
    		
    	}

		//if pacman touch a dot, remove it and subtract to the total count
    	if(collider.gameObject.tag == "Dot")
    	{
    		gameManager.GetComponent<GameManagerScript>().EatDot(false);
    		collider.gameObject.SetActive(false);
    	}

		//if pacman touch a superdot, remove it and subtract to the total count and activate superdot mode
    	if(collider.gameObject.tag == "SuperDot")
    	{
    		gameManager.GetComponent<GameManagerScript>().EatDot(true);
    		collider.gameObject.SetActive(false);
    	}

    }

}
