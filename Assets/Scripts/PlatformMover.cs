using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public float moveDistance = 3f;     // How far it moves from the start point
    public float moveSpeed = 2f;        // How fast it moves

    private Vector3 startPosition;
    private int direction;

    void Start()
    {
        startPosition = transform.position;

        // 🔒 Ensure randomized values are non-zero
        moveDistance = Random.Range(1.5f, 5f); // Minimum 1.5f ensures > 0
        moveSpeed = Random.Range(1f, 3f);      // Minimum 1f ensures > 0

        // Start going left or right randomly
        direction = Random.value < 0.5f ? -1 : 1;

        // ✅ Safety fallback if somehow still zero
        if (moveDistance <= 0f) moveDistance = 1f;
        if (moveSpeed <= 0f) moveSpeed = 1f;
    }

    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * direction * Time.deltaTime);

        // 🔁 Reverse direction at limits
        if (Mathf.Abs(transform.position.x - startPosition.x) >= moveDistance)
        {
            direction *= -1;
        }
    }

    // ✅ Extra safety: validate values if changed in Inspector
    void OnValidate()
    {
        if (moveDistance <= 0f)
        {
            Debug.LogWarning($"{name}: moveDistance was <= 0. Resetting to 1.");
            moveDistance = 1f;
        }

        if (moveSpeed <= 0f)
        {
            Debug.LogWarning($"{name}: moveSpeed was <= 0. Resetting to 1.");
            moveSpeed = 1f;
        }
    }
}
