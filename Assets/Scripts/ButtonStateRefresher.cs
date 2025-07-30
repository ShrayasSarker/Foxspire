using UnityEngine;
using UnityEngine.UI;

public class ButtonStateRefresher : MonoBehaviour
{
    private Button button;

    void OnEnable()
    {
        button = GetComponent<Button>();
        StartCoroutine(RefreshButtonState());
    }

    System.Collections.IEnumerator RefreshButtonState()
    {
        if (button != null)
        {
            button.interactable = false;
            yield return null; // wait 1 frame
            button.interactable = true;
        }
    }
}
