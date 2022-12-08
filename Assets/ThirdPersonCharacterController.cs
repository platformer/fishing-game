using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class ThirdPersonCharacterController : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
	[SerializeField] private float normalSensitivity;
	[SerializeField] private float aimSensitivity;
	[SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
	[SerializeField] private Transform debugTransform;
	[SerializeField] private Transform pfBobberProjectile;
	[SerializeField] private Transform spawnBobberPosition;
	
	private ThirdPersonController thirdPersonController; 
	private StarterAssetsInputs starterAssetsInputs;
	bool canFish = true;

	public void Awake()
	{
		thirdPersonController = GetComponent<ThirdPersonController>();
		starterAssetsInputs = GetComponent<StarterAssetsInputs>();
	}

	private void Update()
	{
		Vector3 mouseWorldPosition = Vector3.zero;
		Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
		Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

		if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
		{
			debugTransform.position = raycastHit.point;
			mouseWorldPosition = raycastHit.point;
		}

		if(starterAssetsInputs.aim)
		{
			aimVirtualCamera.gameObject.SetActive(true);
			Vector3 worldAimTarget = mouseWorldPosition;
			worldAimTarget.y = transform.position.y;
			Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
			thirdPersonController.SetSensitivity(aimSensitivity);
			thirdPersonController.SetRotateOnMove(false);
			transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
		}
		else
		{
			thirdPersonController.SetSensitivity(normalSensitivity);
			thirdPersonController.SetRotateOnMove(true);
			aimVirtualCamera.gameObject.SetActive(false);
		}	

		if(starterAssetsInputs.fish && canFish == true)
		{
			Vector3 aimDir = (mouseWorldPosition - spawnBobberPosition.position).normalized;
			Instantiate(pfBobberProjectile, spawnBobberPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
			starterAssetsInputs.fish = false;
			canFish = false;
			StartCoroutine(FishDelay());
		}
		
	}

	IEnumerator FishDelay()
  	 {
     		yield return new WaitForSeconds(1.5f);
    		canFish = true;
   	}
}
