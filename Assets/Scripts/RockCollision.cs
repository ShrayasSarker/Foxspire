using UnityEngine;

public class RockCollision : MonoBehaviour
{
    private bool isFallingTooLow = false;
    private Vector2 startPosition;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position; // Save starting position
    }

    void Update()
    {
        if (!isFallingTooLow && transform.position.y <= 8f)
        {
            isFallingTooLow = true;
            Invoke("DestroyRock", 2f);
        }
    }

void DestroyRock()
{
    gameObject.SetActive(false); // This hides it, but keeps the reference
}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<PlayerMovement2>().GameOverByRock();
        }
    }

    // 👇 Called by player to reset rock
public void ResetRock()
{
    if (rb == null) rb = GetComponent<Rigidbody2D>();

    transform.position = startPosition;
    rb.linearVelocity = Vector2.zero;
    rb.angularVelocity = 0f;
    isFallingTooLow = false;
    gameObject.SetActive(true); // Reactivate the rock
}

}
