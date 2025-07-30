using UnityEngine;

public class RockTrigger : MonoBehaviour
{
    public Rigidbody2D rockRigidbody;
    public float rollForce = 800f;

    private BoxCollider2D triggerCollider;
    private Vector2 rockStartPosition;

    void Start()
    {
        triggerCollider = GetComponent<BoxCollider2D>();
        if (rockRigidbody != null)
        {
            rockStartPosition = rockRigidbody.transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RollRock();
            triggerCollider.enabled = false; // Disable to prevent retriggering
        }
    }

    void RollRock()
    {
        rockRigidbody.AddForce(Vector2.left * rollForce);
    }

    // 👇 Call this to reset the trigger & rock position
    public void ResetTrigger()
    {
        triggerCollider.enabled = true;

        if (rockRigidbody != null)
        {
            rockRigidbody.transform.position = rockStartPosition;
            rockRigidbody.linearVelocity = Vector2.zero;
            rockRigidbody.angularVelocity = 0f;
        }
    }
}
