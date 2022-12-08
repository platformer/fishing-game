using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingTarget : MonoBehaviour
{
    // Start is called before the first frame update

	public static FishingTarget instance;
	[SerializeField]
	private int health;
	
	public int Health => health;

   	public void Awake()
	{
		if(instance == null)
			instance = this;
	}


    	// Update is called once per frame
    	void Update()
    	{
        
    	}
	
	public void Hit()
	{
		health--;
	}
	
	public int getHealth()
	{
		return health;
	}
}
