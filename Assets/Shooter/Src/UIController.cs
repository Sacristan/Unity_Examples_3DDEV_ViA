using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour 
{
	[SerializeField] private GameObject gameOverPanel;
	[SerializeField] private Text healthUIText;

	private void Start()
	{
		ShooterGameManager.OnPlayerReceivedDamage += ShooterGameManager_OnPlayerReceivedDamage;
		ShooterGameManager.OnPlayerDied += ShooterGameManager_OnPlayerDied;
		EnableGameOverPanel (false);
	}

	void ShooterGameManager_OnPlayerReceivedDamage (int health)
	{
		UpdateHealthUI (health);
	}

	public void LaunchMainMenu()
	{
		SceneManager.LoadScene (Global.MainMenuSceneIndex);
	}

	public void RetryGame()
	{
		SceneManager.LoadScene (Global.GameSceneIndex);
	}

	void ShooterGameManager_OnPlayerDied ()
	{
		ShooterGameManager.OnPlayerDied -= ShooterGameManager_OnPlayerDied;
		EnableGameOverPanel (true);
	}

	private void EnableGameOverPanel(bool flag){
		gameOverPanel.SetActive (flag);
	}

	private void UpdateHealthUI(int health)
	{
		if(healthUIText!=null) healthUIText.text = health.ToString();
	}




}
