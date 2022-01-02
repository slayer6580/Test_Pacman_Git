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

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input for mouvement
    	float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        direction = (transform.right * moveX + transform.forward * moveY);

        controller.Move(direction * speed * Time.deltaTime);
    }
}
