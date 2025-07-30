using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isUsed = false; // Prevents double use

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isUsed) return;

        if (other.CompareTag("Player"))
        {
            PlayerMovement2 player = other.GetComponent<PlayerMovement2>();
            if (player != null)
            {
                player.restartPosition = transform.position;
                player.SaveCheckpointHealth();
                Debug.Log("✅ Checkpoint saved: position + health");

                isUsed = true; // Mark as used
                // Disable collider only
                GetComponent<Collider2D>().enabled = false;

            }
        }
    }
}
