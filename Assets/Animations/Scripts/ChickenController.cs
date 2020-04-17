using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour 
{
	private const float HeadBangingTimeInSeconds = 31f;

	private Animator _animator;

	void Start () 
	{
		_animator = GetComponent<Animator> ();
		StartCoroutine (HeadBangRoutine ());
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			_animator.SetBool ("IsDead",true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			_animator.SetBool ("IsDead",false);
		}
	}

	private IEnumerator HeadBangRoutine()
	{
		yield return new WaitForSeconds (31f);
		_animator.SetBool ("IsHeadBanging",true);
	}

}
