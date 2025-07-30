using UnityEngine;

public class Zem : MonoBehaviour
{
    private bool collected = false; // 👈 prevents multiple triggers

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return; // 👈 stop if already collected
        if (other.CompareTag("Player"))
        {
            collected = true; // ✅ lock further triggers
            ZemManager.instance.CollectZem();
            Destroy(gameObject); // or disable instead
        }
    }
}

