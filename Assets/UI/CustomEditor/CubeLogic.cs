using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLogic : MonoBehaviour {

	[SerializeField] private float speed = 1f;
	void Update () {
		transform.position = Vector3.one * Mathf.Sin(Time.time) * speed;
	}
}
