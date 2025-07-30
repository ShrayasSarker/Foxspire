using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject player; // Drag your player GameObject here

    private bool isPaused = false;
    private Animator playerAnimator;

    void Start()
    {
        if (player != null)
            playerAnimator = player.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        if (playerAnimator != null)
            playerAnimator.enabled = true;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        if (playerAnimator != null)
            playerAnimator.enabled = false;
        isPaused = true;
    }

    public void Menu()
    {
        Resume(); // Ensure the game is resumed before going to the menu
        SceneManager.LoadScene("Menu"); // Replace "MainMenu" with your actual menu scene name
    }
}
