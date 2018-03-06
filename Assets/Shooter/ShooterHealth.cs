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
        //Lets finish this once UI is done
        throw new System.NotImplementedException();
    }
}