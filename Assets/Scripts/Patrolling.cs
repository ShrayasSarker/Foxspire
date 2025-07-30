using UnityEngine;
using Pathfinding;

public class patrolling : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPoint = 0;

    public Transform player;
    public float detectionRadius = 5f;

    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;

    private AIPath aiPath;
    private AIDestinationSetter destinationSetter;
    private bool chasing = false;

    void Start()
    {
        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();

        if (patrolPoints.Length > 0)
        {
            destinationSetter.target = patrolPoints[currentPoint];
            aiPath.maxSpeed = patrolSpeed;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        float yDifference = Mathf.Abs(player.position.y - transform.position.y);

        // Only chase if on similar vertical level (to avoid flying bear)
        if (distanceToPlayer < detectionRadius && yDifference < 1.5f)
        {
            if (!chasing)
            {
                chasing = true;
                destinationSetter.target = player;
                aiPath.maxSpeed = chaseSpeed;
            }
        }
        else
        {
            if (chasing)
            {
                chasing = false;
                destinationSetter.target = patrolPoints[currentPoint];
                aiPath.maxSpeed = patrolSpeed;
            }

            // Continue patrolling
            if (!aiPath.pathPending && aiPath.reachedDestination)
            {
                currentPoint = (currentPoint + 1) % patrolPoints.Length;
                destinationSetter.target = patrolPoints[currentPoint];
            }
        }

        FlipBasedOnDirection();
    }

    void FlipBasedOnDirection()
    {
        if (aiPath.desiredVelocity.x > 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (aiPath.desiredVelocity.x < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
