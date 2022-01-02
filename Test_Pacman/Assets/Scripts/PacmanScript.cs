using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanScript : MonoBehaviour
{
	private float moveX;
	private float moveY;

	[SerializeField] 
	private float speed;
	private Vector3 direction;
	private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
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
    		print("mort");
    	}

    }
}
