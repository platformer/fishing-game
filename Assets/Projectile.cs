using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	private Rigidbody bobberRigidbody;
	private bool collision = false;

	private void Awake() {
		bobberRigidbody = GetComponent<Rigidbody>();
	}
	
	private void Start() {
		float speed = 10f;
		bobberRigidbody.velocity = transform.forward * speed;
	}
	
	private void OnTriggerEnter(Collider other)
	{
		//hit a fish
		if(other.GetComponent<FishingTarget>() != null && collision == false)
		{
			collision = true;
			FishingTarget.instance.Hit();
			int fishHealth = FishingTarget.instance.getHealth();
			if(fishHealth <= 0)
			{
				Destroy(other.gameObject);
				FishCaught.instance.FishCatch();
			}
			Destroy(gameObject); 
		}
		else	//hit something that's not a fish
		{
			Destroy(gameObject);
	        }
	}
	
}
