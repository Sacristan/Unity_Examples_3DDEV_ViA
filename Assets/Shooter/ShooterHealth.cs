using UnityEngine;

public class ShooterHealth : MonoBehaviour, IDamageable
{
    private int health = 100;

    public void ApplyDamage(int damage)
    {
        Debug.Log("Player received damage: " + damage);
        health = Mathf.Clamp(health - damage, 0, 100);

        if (health <= 0) Die();
    }

    public void Die()
    {
        Debug.Log("GAMEOVER");
        Debug.Break();

        //Lets finish this once UI is done
    }
}