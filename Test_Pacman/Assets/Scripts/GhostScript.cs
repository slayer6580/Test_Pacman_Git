using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{	
	[SerializeField]
	private UnityEngine.AI.NavMeshAgent agent;

	public state behaviour;
	[SerializeField]
	public enum state 
	{
  		wander, chase, flee, respawn
	}

    [SerializeField]
	private float fleeDistance;

    [SerializeField]
	private float normalSpeed;
    [SerializeField]
	private float respawnSpeed;

	[SerializeField]
	private Material baseColor;
	[SerializeField]
	private Material fleeColor;

	private GameObject player;
	private GameObject wanderWaypoints;
    private GameObject respawn;
	private Vector3 position;
	private bool goingToWaypoint;



    void Start()
    {
        player = GameObject.Find("Pacman");
        wanderWaypoints = GameObject.Find("WanderWaypoints");
        respawn = GameObject.Find("Respawn");
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

        case state.respawn:
            GoToRespawn();
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

        /**/

        if(goingToWaypoint == false)
    	{
    		goingToWaypoint = true;
            int _waypointToGo = Mathf.RoundToInt(Random.Range(0, wanderWaypoints.transform.childCount));
            position = wanderWaypoints.gameObject.transform.GetChild(_waypointToGo).transform.position;

            while(Vector3.Distance(player.transform.position, position) <= fleeDistance)
            {
                _waypointToGo = Mathf.RoundToInt(Random.Range(0, wanderWaypoints.transform.childCount));
                position = wanderWaypoints.gameObject.transform.GetChild(_waypointToGo).transform.position;
            }

    	}
    	agent.SetDestination(position);

    	if(Vector3.Distance(position, transform.position) <= 1)
    	{
			goingToWaypoint = false;
    	}

        Debug.DrawRay(position, player.transform.position-transform.position, Color.green);
        //print(player.transform.position-transform.position);
        //print(Vector3.Distance(transform.position, player.transform.position));
    }



    public void SuperDot()
    {
        if(behaviour != state.respawn)
        {
            behaviour = state.flee;
            goingToWaypoint = false;
        }
    }

    public void GoToRespawn()
    {
        agent.SetDestination(respawn.transform.position);
        agent.speed = 10f;
        if(Vector3.Distance(position, transform.position) <= 1)
    	{
            behaviour = state.wander;
			goingToWaypoint = false;
            agent.speed = 2;
    	}

    }
}
