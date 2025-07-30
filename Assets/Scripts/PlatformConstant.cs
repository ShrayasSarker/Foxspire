using UnityEngine;

public class PlatformConstant : MonoBehaviour
{
    public float moveDistance = 3f;   // How far it moves left/right from start
    public float moveSpeed = 2f;      // How fast it moves
    public bool startRight = true;    // Start direction

    private Vector3 startPos;
    private int direction;

    void Start()
    {
        startPos = transform.position;
        direction = startRight ? 1 : -1;
    }

    void Update()
    {
        // Move platform left or right
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

        // Reverse direction when reaching the distance
        if (Mathf.Abs(transform.position.x - startPos.x) >= moveDistance)
        {
            direction *= -1;
        }
    }
}
