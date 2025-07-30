using UnityEngine;
using TMPro;
public class ZemManager : MonoBehaviour
{
    public static ZemManager instance;

    private int zemCount = 0;
    public int requiredZems = 10;
    public TextMeshProUGUI zemCounterText; // Drag your TMP text in Inspector

    public Collider2D finishLineCollider; // Assign the FinishLine's collider in Inspector

    private void Start()
{
    UpdateZemUI(); // set to 0/8 at beginning
}
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (finishLineCollider != null)
        {
            finishLineCollider.enabled = false; // Lock the finish at start
        }
    }

public void CollectZem()
{
    zemCount++;
    Debug.Log($"Zem collected! Total: {zemCount}/{requiredZems}");

    UpdateZemUI(); // 👈 Update UI every time
    if (zemCount >= requiredZems)
    {
        UnlockFinish();
    }
}
private void UpdateZemUI()
    {
        if (zemCounterText != null)
        {
            zemCounterText.text = $"{zemCount}/{requiredZems}";
        }
    }

    private void UnlockFinish()
    {
        if (finishLineCollider != null)
        {
            finishLineCollider.enabled = true;
            Debug.Log("✅ All Zems collected! Finish line unlocked.");
        }
    }

    public bool AreAllZemsCollected()
    {
        return zemCount >= requiredZems; // ✅ Manual version uses requiredZems
    }
}
