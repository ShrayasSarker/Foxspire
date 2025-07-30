using UnityEngine;
using TMPro;

public class FinishBlocker : MonoBehaviour
{
    public TextMeshProUGUI warningMessage;
    public float warningDuration = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (ZemManager.instance != null && ZemManager.instance.AreAllZemsCollected())
            {
                // ✅ All Zems collected: disable this blocker
                gameObject.SetActive(false);
            }
            else
            {
                // ❌ Not collected: show warning
                if (warningMessage != null)
                {
                    StopAllCoroutines();
                    StartCoroutine(ShowWarning());
                }
            }
        }
    }

    private System.Collections.IEnumerator ShowWarning()
    {
        warningMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(warningDuration);
        warningMessage.gameObject.SetActive(false);
    }
}
