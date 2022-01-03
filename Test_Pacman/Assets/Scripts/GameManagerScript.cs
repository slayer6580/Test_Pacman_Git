using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
	public GameObject DotsGroup;

	private int DotsCount;
    // Start is called before the first frame update
    void Start()
    {
        DotsCount = DotsGroup.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EatDot()
    {

    }
}
