using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishCaught : MonoBehaviour
{
	public static FishCaught instance;

	[SerializeField]
	private Text fishCaughtCountTxt;
	private int fishCaughtCount = 0;
	

	public void Awake()
	{
		if(instance == null)
			instance = this;
	}

	public void FishCatch()
	{
		fishCaughtCount++;
		fishCaughtCountTxt.text = "Fish Caught: " +fishCaughtCount;
	}
		
}
