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
	private GameObject wanderWaypoints;
	private Vector3 position;
	private bool goingToWaypoint;


    void Start()
    {
        player = GameObject.Find("Pacman");
        wanderWaypoints = GameObject.Find("WanderWaypoints");
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
    	if(goingToWaypoint == false)
    	{
    		goingToWaypoint = true;
    		int _waypointToGo = Mathf.RoundToInt(Random.Range(0, wanderWaypoints.transform.childCount));
    		position = wanderWaypoints.gameObject.transform.GetChild(_waypointToGo).transform.position;
    	}
    	agent.SetDestination(position);

    	if(Vector3.Distance(position, transform.position) <= 1)
    	{
			goingToWaypoint = false;
    	}
    	
    }

    private void Chase()
    {
    	agent.SetDestination(player.transform.position);
    }

    private void Flee()
    {

    }
}
