using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

	[SerializeField]
	private BulletShooter bulletPrefab;

	[SerializeField]
	private Transform spawnTransform;

	[SerializeField]
	private float bulletForce = 1f;

	[SerializeField]
	private int fireFrameRate = 5;

	[SerializeField]
	private Transform gunPivot;

	[SerializeField]
	private float recoilForce = 0.1f;

	[SerializeField]
	private float recoilRecoveryTimeInSeconds = 1.5f;

	private float recoil;

	private void Update () {

		if (Input.GetMouseButton (0)) 
		{
			Fire ();
		}

		recoil += Time.deltaTime / recoilRecoveryTimeInSeconds;
		gunPivot.localPosition = Vector3.forward * Mathf.Clamp(recoil, -recoilForce, 0f) ;
	}

	private void Fire()
	{
		if (Time.frameCount % fireFrameRate == 0) 
		{
			recoil = -recoilForce;

			Instantiate (bulletPrefab, spawnTransform.position, spawnTransform.rotation);
		}
	}
}
