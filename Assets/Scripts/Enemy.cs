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
    public Animator anim;

    // Aggression and Wanderer Reference
    public Wanderer wanderer; // Reference to the Wanderer script
    public float campRange = 15f; // Range of the enemy camp
    private NavMeshAgent agent; // NavMeshAgent for movement

    public bool isAggressive = false; // Flag to control aggression


    private void Start()
    {
        anim = GetComponent<Animator>();

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
            isAggressive = true;

            // Stop patrol behavior if applicable
            DemonPatrol patrolScript = GetComponent<DemonPatrol>();
            if (patrolScript != null)
            {
                patrolScript.enabled = false;
            }

            // Aggressive behavior: move toward Wanderer
            agent.SetDestination(wanderer.transform.position);

            if (distanceToWanderer <= agent.stoppingDistance)
            {
                Attack();
            }
        }
        else
        {
            isAggressive = false;

            // Resume patrol behavior if applicable
            DemonPatrol patrolScript = GetComponent<DemonPatrol>();
            if (patrolScript != null)
            {
                patrolScript.enabled = true;
            }
        }
    }

    private void Attack()
    {
        Debug.Log(enemyType + " attacks Wanderer!");
        anim.SetBool("punching", true);
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
