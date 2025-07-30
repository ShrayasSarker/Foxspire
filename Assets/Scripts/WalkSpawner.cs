using UnityEngine;

public class WalkSpawner : MonoBehaviour
{
    public GameObject spawnPrefab;
    public float spawnInterval = 0.5f;
    public float moveThreshold = 0.1f;
    public float destroyDelay = 1.5f;
    public AudioSource footstepSound;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private float timer = 0f;
    private Vector2 lastPosition;
    private bool isGrounded;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        // Ground detection
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        float movement = Vector2.Distance(transform.position, lastPosition);
        timer += Time.deltaTime;

        if (movement > moveThreshold && timer >= spawnInterval && isGrounded)
        {
            GameObject spawned = Instantiate(spawnPrefab, transform.position, Quaternion.identity);
            Destroy(spawned, destroyDelay);

            if (footstepSound != null && !footstepSound.isPlaying)
            {
                footstepSound.Play();
            }

            timer = 0f;
        }

        lastPosition = transform.position;
    }

    void OnDrawGizmosSelected()
    {
        // Optional: visualize ground check radius
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
