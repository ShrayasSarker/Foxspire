using UnityEngine;
using Pathfinding;
public class PlayerDitectionZoneBat : MonoBehaviour
{
    public Animator animator;          // Animator from Eagle
    public GameObject enemy;           // Parent enemy GameObject
    public Transform playerTarget;     // Assign player Transform in Inspector

    private AIDestinationSetter destSetter;
    private AIPath aiPath;
    private Transform homePosition;

    public float stopDistance = 0.1f;  // How close to home before eagle stops flying

    private void Start()
    {
        destSetter = enemy.GetComponent<AIDestinationSetter>();
        aiPath = enemy.GetComponent<AIPath>();

        // Create home position
        homePosition = new GameObject("EnemyHome").transform;
        homePosition.position = enemy.transform.position;
        homePosition.hideFlags = HideFlags.HideInHierarchy;

        // Start at home
        destSetter.target = homePosition;
    }

    private void Update()
    {
        if (destSetter.target == homePosition)
        {
            float distance = Vector2.Distance(enemy.transform.position, homePosition.position);

            if (distance <= stopDistance)
            {
                if (animator != null)
                    animator.SetBool("IsFlying", false); // Eagle reached home, stop flying
            }
            else
            {
                if (animator != null)
                    animator.SetBool("IsFlying", true); // Still flying home
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            destSetter.target = playerTarget;
            aiPath.enabled = true;
            destSetter.enabled = true;

            if (animator != null)
                animator.SetBool("IsFlying", true); // Start flying toward player
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            destSetter.target = homePosition;
            aiPath.enabled = true;
            destSetter.enabled = true;

            // DO NOT stop flying here — we handle that in Update()
        }
    }
}
