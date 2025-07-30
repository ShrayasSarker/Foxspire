using UnityEngine;
using Pathfinding;

public class BatGFX : MonoBehaviour
{
public PlayerDitectionZoneBat detectionZoneBat;
    public Animator animator; // Animator for enemy animations
    public AIPath aiPath;
    Vector3 originalScale;
    float EagleSpeed;// Speed of the eagle, adjust as needed
    public
    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Nest"))
        {
            animator.SetFloat("IsFlying", 0);// Set animation state to idle
        }
    }
}
