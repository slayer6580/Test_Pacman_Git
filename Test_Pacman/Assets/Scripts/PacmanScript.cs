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


    private void OnTriggerEnter(Collider collider)
    {
    	if(collider.gameObject.tag == "Ghost"){
			if(superDotMode == true)
			{
				collider.gameObject.GetComponent<GhostScript>().behaviour = GhostScript.state.respawn;
			}
			else
			{
				gameManager.EndGame(false);;
			}
    		
    	}

    	if(collider.gameObject.tag == "Dot")
    	{
    		gameManager.GetComponent<GameManagerScript>().EatDot(false);
    		collider.gameObject.SetActive(false);
    	}

    	if(collider.gameObject.tag == "SuperDot")
    	{
    		gameManager.GetComponent<GameManagerScript>().EatDot(true);
    		collider.gameObject.SetActive(false);
    	}

    }

}
