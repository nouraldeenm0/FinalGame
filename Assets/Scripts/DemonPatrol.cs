using UnityEngine;
using UnityEngine.AI;

public class DemonPatrol : MonoBehaviour
{
    public Transform[] patrolPoints; // Array of waypoints for the patrol
    private int currentPatrolIndex = 0; // Current patrol point index
    private NavMeshAgent agent; // NavMeshAgent for movement
    private Animator animator; // Animator for demon walking animation

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    private void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            // Move to the next patrol point
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }

        // Update animation
        if (animator != null)
        {
            animator.SetBool("isWalking", agent.velocity.magnitude > 0.1f);
        }
    }
}
