using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
	//public
	public GameObject DotsGroup;
	public GameObject[] GhostGroup;
	public GameObject player;
	public float timeSuperDot;

	//private 
	private int DotsCount;
	private int superDotActive;


    void Start()
    {
        DotsCount = DotsGroup.transform.childCount;
        GhostGroup = GameObject.FindGameObjectsWithTag("Ghost");
		player = GameObject.Find("Pacman");
    }

	/*
	Manage the count of dots in the level and when its a superdot
	*/
    public void EatDot(bool isSuperDot)
    {
    	--DotsCount;
    	if(DotsCount == 0)
    	{
    		EndGame();
    	}

    	if(isSuperDot == true)
    	{
    		for(int i = 0; i < GhostGroup.Length; i++)
    		{
    			GhostGroup[i].GetComponent<GhostScript>().SuperDot();
    		}
			StartCoroutine("SuperDotMode");
			superDotActive++;
			player.GetComponent<PacmanScript>().superDotMode = true;
    	}
    }

	/*
	Manage the superdot mode with the ghost ans pacman when PacMan eats a SuperDot
	*/
	IEnumerator SuperDotMode()
	{
		yield return new WaitForSeconds(timeSuperDot);
		superDotActive--;
		if(superDotActive <= 0){
			player.GetComponent<PacmanScript>().superDotMode = false;
			for(int i = 0; i < GhostGroup.Length; i++)
    		{
    			if(GhostGroup[i].GetComponent<GhostScript>().behaviour == GhostScript.state.flee)
				{
					GhostGroup[i].GetComponent<GhostScript>().behaviour = GhostScript.state.wander;
					GhostGroup[i].GetComponent<GhostScript>().UpdateToBaseColor();
				}
    		}
		}
		
	}
	
	/*
	Change scene to GameOver scene
	*/
	public void EndGame(){
		SceneManager.LoadScene("GameOver");
	}
}
