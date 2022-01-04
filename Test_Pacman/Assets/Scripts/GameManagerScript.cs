using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
	public GameObject DotsGroup;
	public GameObject[] GhostGroup;
	public GameObject player;
	public float timeSuperDot;


	private int DotsCount;
	private int superDotActive;


    void Start()
    {
        DotsCount = DotsGroup.transform.childCount;
        GhostGroup = GameObject.FindGameObjectsWithTag("Ghost");
		player = GameObject.Find("Pacman");
    }

    public void EatDot(bool isSuperDot)
    {
    	--DotsCount;
    	if(DotsCount == 0)
    	{
    		EndGame(true);
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

	public void EndGame(bool victory){
		if(victory = true){

		}else{
			
		}
		
	}
}
