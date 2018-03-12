using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour 
{
	public void StartGame()
	{
		SceneManager.LoadScene (Global.GameSceneIndex);	
	}

	public void QuitGame()
	{
		Application.Quit ();
	}
}
