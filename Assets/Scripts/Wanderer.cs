using UnityEngine;

public class Wanderer : MonoBehaviour
{
    public int health = 100;      // Wanderer's starting health
    public int experience = 0;   // Wanderer's XP

    // Called when the Wanderer takes damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Wanderer took damage! Health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    // Called when the Wanderer gains XP
    public void GainXP(int xp)
    {
        experience += xp;
        Debug.Log("Wanderer gained XP! Total XP: " + experience);
    }

    private void Die()
    {
        Debug.Log("Wanderer has died!");
        // Add any logic for what happens when the Wanderer dies (e.g., reload level).
    }
}
