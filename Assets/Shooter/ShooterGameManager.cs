using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterGameManager : MonoBehaviour {
	private static ShooterGameManager instance;

	private GameObject _player;

	public static GameObject Player
	{
		get
		{
			if (instance._player == null) 
			{
				instance._player = GameObject.FindGameObjectWithTag ("Player");
			}

			return instance._player;
		}
	
	}

	void Awake () 
	{
		instance = this;	
	}
	
}
