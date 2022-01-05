using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{	
    //private
	[SerializeField]
	private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField]
	private float fleeDistance;
    [SerializeField]
	private float normalSpeed;
    [SerializeField]
	private float respawnSpeed;
    [SerializeField]
	private float timeInWander;
    [SerializeField]
	private float timeInChase;
	[SerializeField]
	private Material baseColor;
	[SerializeField]
	private Material fleeColor;
    [SerializeField]
	private Material respawnColor;
	private GameObject player;
	private GameObject wanderWaypoints;
    private GameObject respawn;
	private Vector3 position;
	private bool goingToWaypoint;
    private MeshRenderer ghostColor;

    //public
    public state behaviour;
	[SerializeField]
	public enum state 
	{
  		wander, chase, flee, respawn
	}
    



    void Start()
    {
        player = GameObject.Find("Pacman");
        wanderWaypoints = GameObject.Find("WanderWaypoints");
        respawn = GameObject.Find("Respawn");
        StartCoroutine("WanderingTime");
        ghostColor = GetComponent<MeshRenderer>();
        ghostColor.material = baseColor;
        agent.speed = normalSpeed;
    }

    void Update()
    {
        /*
        Manage the behaviour of the ghost
        */
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


    /*
    When the ghost is wandering, to go random point on the map
    */
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

    /*
    When in chase mode, go to player position
    */
    private void Chase()
    {
    	agent.SetDestination(player.transform.position);
    }

    /*
    Flee the player when he eats a superdot 
    */
    private void Flee()
    {   
        ghostColor.material = fleeColor;
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

    	if(Vector3.Distance(position, transform.position) <= 1f)
    	{
			goingToWaypoint = false;
    	}

    }

    /*
    If the ghost is already going for the respawn, change their state to flee
    */
    public void SuperDot()
    {
        if(behaviour != state.respawn)
        {
            behaviour = state.flee;
            goingToWaypoint = false;
            ghostColor.material = fleeColor;
        }
    }

    /*
    When the ghost is being touched by pacman while in superdot mode, go to respawn
    */
    public void GoToRespawn()
    {
        ghostColor.material = respawnColor;
        agent.SetDestination(respawn.transform.position);
        agent.speed = respawnSpeed;
        if(Vector3.Distance(respawn.transform.position, transform.position) <= 1f)
    	{
            goingToWaypoint = false;
            behaviour = state.wander;
            agent.speed = normalSpeed;
            ghostColor.material = baseColor;
    	}

    }

    /*
    After x time of wandering, go to chase mode 
    */
    IEnumerator WanderingTime()
    {
        if(behaviour == state.chase)
        {
            behaviour = state.wander;
        }
        yield return new WaitForSeconds(timeInWander);
        StartCoroutine("ChaseTime");

    }

    /* 
    After x time of chasing pacman, got o wandering mode
    */
    IEnumerator ChaseTime()
    {
        if(behaviour == state.wander)
        {
            behaviour = state.chase;
        }
        yield return new WaitForSeconds(timeInChase);
        StartCoroutine("WanderingTime");
    }

    /* 
    Actvated after Superdot mode is done
    */
    public void UpdateToBaseColor(){
        ghostColor.material = baseColor;
    }



}
