using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Enemy Types
    public enum EnemyType { Minion, Demon }
    public EnemyType enemyType;

    // Stats
    public int healthPoints;
    public int xpValue;
    public int damage;

    // Aggression and Wanderer Reference
    public Wanderer wanderer;    // Reference to the Wanderer script
    public float campRange = 15f; // Range of the enemy camp
    private NavMeshAgent agent;   // NavMeshAgent for movement

    private void Start()
    {
        // Assign stats based on enemy type
        if (enemyType == EnemyType.Minion)
        {
            healthPoints = 20;
            xpValue = 10;
            damage = 5;
        }
        else if (enemyType == EnemyType.Demon)
        {
            healthPoints = 40;
            xpValue = 30;
            damage = 10; // Default attack damage
        }

        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (wanderer == null) return;

        float distanceToWanderer = Vector3.Distance(transform.position, wanderer.transform.position);

        if (distanceToWanderer <= campRange)
        {
            // Aggressive behavior: move toward Wanderer
            agent.SetDestination(wanderer.transform.position);

            if (distanceToWanderer <= agent.stoppingDistance)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        Debug.Log(enemyType + " attacks Wanderer!");
        wanderer.TakeDamage(damage);
    }

    public void TakeDamage(int damageAmount)
    {
        healthPoints -= damageAmount;
        Debug.Log(enemyType + " took damage! Health: " + healthPoints);

        if (healthPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(enemyType + " died! Wanderer gains " + xpValue + " XP.");
        wanderer.GainXP(xpValue);
        Destroy(gameObject);
    }
}
