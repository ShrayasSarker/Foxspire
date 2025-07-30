using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishLine : MonoBehaviour
{
    public GameObject GameComplete;               // UI Panel shown when game is complete
    public Animator GameCompleteAnimator;         // Animator to show GameComplete panel
    public float delayBeforePause = 1.5f;         // Delay before pausing the game

    public TextMeshProUGUI warningMessage;        // 👈 Drag the TMP UI text here in Inspector
    public float warningDuration = 2f;            // How long to show the message

    public GameObject textToDisable;              // Drag UI text shown during play
    public GameObject movementUI;                 // 👈 Drag the joystick / movement buttons here
    public GameObject movementUI2;
    public GameObject resumeButton;               // 👈 Drag your Resume button here in Inspector

    private GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (ZemManager.instance != null && !ZemManager.instance.AreAllZemsCollected())
            {
                if (warningMessage != null)
                {
                    StopAllCoroutines();
                    StartCoroutine(ShowWarningMessage());
                }
                return; // Don't finish the game yet
            }

            player = other.gameObject;
            StartCoroutine(FinishGame());

            if (textToDisable != null)
                textToDisable.SetActive(false);

            if (movementUI != null)
                movementUI.SetActive(false); // 👈 Hide movement UI on finish

            if (movementUI2 != null)
                movementUI2.SetActive(false); // 👈 Hide second movement UI on finish

            if (resumeButton != null)
                resumeButton.SetActive(false); // 👈 Hide resume button on finish
        }
    }

    private System.Collections.IEnumerator FinishGame()
    {
        if (GameComplete != null)
            GameComplete.SetActive(true);

        if (GameCompleteAnimator != null)
            GameCompleteAnimator.SetTrigger("Show");

        if (player != null)
            player.SetActive(false);

        Debug.Log("🎉 You reached the finish line!");
        yield return new WaitForSecondsRealtime(delayBeforePause);

        Time.timeScale = 0f; // Pause game after delay
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    private System.Collections.IEnumerator ShowWarningMessage()
    {
        warningMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(warningDuration);
        warningMessage.gameObject.SetActive(false);
    }
}
