using UnityEngine;

public class Cherry : MonoBehaviour
{
private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // Makes the cherry vanish
        }
    }
}
