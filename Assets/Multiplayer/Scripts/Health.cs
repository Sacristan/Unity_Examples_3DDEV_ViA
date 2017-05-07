using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour
{
    public const int maxHealth = 100;

    [SyncVar]
    private int currentHealth = maxHealth;

    public RectTransform healthBar;

    public void TakeDamage(int amount)
    {
        if (!isLocalPlayer) return;

        Debug.Log("TakeDamage received from Server");

        currentHealth -= amount;

        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);

        if (currentHealth <= 0) Destroy(gameObject);

    }
}