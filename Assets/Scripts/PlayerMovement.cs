using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement2 : MonoBehaviour
{
    public Cherry cherry;
    public int maxhealth = 100;
    public int currenthealth;
    public HealthBar healthBar;
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    public float deathY = -10f;
    public float RestartDelay = 1f; // Set this in the Inspector or keep default

    public AudioSource audioSource; // 🔈 Sound when hit
    public AudioClip hitSound; // 🎵 Assign this from Inspector
    public Vector2 restartPosition; // Position to restart from when hitting a checkpoint
    private int savedHealth; // 👈 Store health when hitting a checkpoint

    public RockCollision rock;
    public RockTrigger rockTrigger;
    public GameObject gameOverUI; // Assign your Game Over UI panel in Inspector
    private bool isGameOver = false;
    private bool isGrounded = true; // Replace with actual ground check if needed

    public Joystick joystick; // Reference to the Joystick script for mobile controls
    public Joystick joystick2; // Reference to the second Joystick script for mobile controls


    void Start()
    {
        currenthealth = maxhealth;
        healthBar.SetMaxHealth(maxhealth);

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>(); // Try to get AudioSource if not set
        }
        savedHealth = currenthealth;
        restartPosition = transform.position;
    }

    void Update()
    {
        if (transform.position.y < deathY)
        {
            GameOver(); // Call GameOver if player falls below deathY
        }
        if (currenthealth <= 0)
        {
            GameOver(); // Call GameOver if health is zero or below
        }

        if (joystick.Horizontal >= .4f)
        {
            horizontalMove = runSpeed;

        }
        else if (joystick.Horizontal <= -.4f)
        {
            horizontalMove = -runSpeed;
        }
        else
        {
            horizontalMove = 0f; // No movement if joystick is centered
        }

        float verticalMove = joystick2.Vertical; // Get vertical movement from joystick


        // Update animator with movement speed
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (verticalMove > 0.6f && isGrounded)
            {
                jump = true;
                animator.SetBool("IsJumping", true);
                isGrounded = false;
            }

        if (verticalMove < -0.6f)
        {
            crouch = true; // Crouch if joystick is pushed down
            animator.SetBool("IsCrouching", true);
            
        }
        else
        {
            crouch = false;

        }

        // Jump input (keyboard)

        // Crouch input (keyboard)
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            animator.SetBool("IsCrouching", true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            animator.SetBool("IsCrouching", false);
        }

        // Restart on Game Over screen
        if (isGameOver)
        {
            if (Input.GetMouseButtonDown(0)) // 👈 checks for a tap or mouse click
            {
                animator.enabled = true;
                Time.timeScale = 1f;
                isGameOver = false;
                gameOverUI.SetActive(false);
                Respawn(); // ⬅️ Respawn at checkpoint
            }

            /*if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.enabled = true;
                Time.timeScale = 1f;
                isGameOver = false;
                gameOverUI.SetActive(false);
                Respawn(); // ⬅️ Respawn at checkpoint
            }*/
            return;
        }
    }

    void TakeDamage(int damage)
    {
        currenthealth -= damage;
        healthBar.SetHealth(currenthealth);

        // 🔊 Play hit sound
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cherry"))
        {
            currenthealth += 25; // Increase health by 25 when colliding with a cherry
            if (currenthealth > maxhealth)
            {
                currenthealth = maxhealth; // Ensure health does not exceed max health
            }
            healthBar.SetHealth(currenthealth);
        }
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(25); // Take damage when colliding with an enemy
        }
        if (other.CompareTag("Traps"))
        {
            GameOver(); // Call GameOver when colliding with traps
        }
    }

    public void OnLanding()
    {
        // Reset the jump animation when landing
        animator.SetBool("IsJumping", false);
        isGrounded = true; // For coyote time
    }

    public void OnCrouching(bool isCrouching)
    {
        // Update the animator to reflect crouching state
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        // Apply movement and reset jump flag
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        isGameOver = true;
        Time.timeScale = 0f; // Pause the game
        animator.enabled = false;
        gameOverUI.SetActive(true); // Show Game Over screen
    }

    void Restart()
    {
        AudioListener.volume = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
        currenthealth = maxhealth; // Reset health
    }

    public void GameOverByRock()
    {
        Debug.Log("Game Over: Hit by rock.");
        GameOver();
    }

    public void Respawn()
    {
        transform.position = restartPosition;
        currenthealth = savedHealth;
        healthBar.SetHealth(currenthealth);

        if (rock != null)
            rock.ResetRock();
        else
            Debug.LogWarning("Rock reference is missing!");

        if (rockTrigger != null)
            rockTrigger.ResetTrigger();
        else
            Debug.LogWarning("RockTrigger reference is missing!");
    }

    public void SaveCheckpointHealth()
    {
        savedHealth = currenthealth;
    }

} 
