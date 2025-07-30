using UnityEngine;
using Pathfinding;

public class EnemyPatrolAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Transform playerTarget;
    public float detectionRange = 5f;
    private int currentPoint = 0;

    private AIDestinationSetter destinationSetter;
    private AIPath aiPath;

    void Start()
    {
        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();

        if (patrolPoints.Length > 0)
        {
            destinationSetter.target = patrolPoints[0];
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTarget.position);

        if (distanceToPlayer <= detectionRange)
        {
            destinationSetter.target = playerTarget;
        }
        else
        {
            // If reached current patrol point, go to next
            if (!aiPath.pathPending && aiPath.reachedDestination)
            {
                currentPoint = (currentPoint + 1) % patrolPoints.Length;
                destinationSetter.target = patrolPoints[currentPoint];
            }
        }
    }
}
