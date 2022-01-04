using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
	public GameObject DotsGroup;

	public GameObject[] GhostGroup;

	private int DotsCount;
    // Start is called before the first frame update
    void Start()
    {
        DotsCount = DotsGroup.transform.childCount;
        GhostGroup = GameObject.FindGameObjectsWithTag("Ghost");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EatDot(bool isSuperDot)
    {
    	--DotsCount;
    	if(DotsCount == 0)
    	{
    		//EndGame
    	}

    	if(isSuperDot == true)
    	{
    		for(int i = 0; i < GhostGroup.Length; i++)
    		{
    			GhostGroup[i].GetComponent<GhostScript>().SuperDot();
    		}
    	}
    }
}
