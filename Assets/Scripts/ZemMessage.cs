using UnityEngine;
using TMPro;

public class ZemMessageUI : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    void Start()
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(true); // Show at start
        }
    }

    void Update()
    {
        if (ZemManager.instance != null && messageText != null)
        {
            if (ZemManager.instance.AreAllZemsCollected())
            {
                messageText.gameObject.SetActive(false); // Hide after collecting all
            }
        }
    }
}


