using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{	
	[SerializeField]
	private UnityEngine.AI.NavMeshAgent agent;

	[SerializeField]
	private state behaviour;

	[SerializeField]
	private enum state 
	{
  		wander, chase, flee
	}

	private GameObject player;


    void Start()
    {
        player = GameObject.Find("Pacman");
    }

    void Update()
    {
    	switch (behaviour)
        {
        case state.wander:
            Wander();
            break;

        case state.chase:
            Chase();
            break;

        case state.flee:
            Flee();
            break;
        }
    }



    private void Wander()
    {

    }

    private void Chase()
    {
    	agent.SetDestination(player.transform.position);
    }

    private void Flee()
    {

    }
}
