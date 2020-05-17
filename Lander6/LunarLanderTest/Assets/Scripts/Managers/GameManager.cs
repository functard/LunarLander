using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static bool gameIsPaused = false;

    [Tooltip("Pause menu reference")]
    [SerializeField]
    private GameObject pauseMenuUI;

    [Tooltip("Retry button reference")]
    [SerializeField]
    private Button retryButton;

    [Tooltip("Quit button reference")]
    [SerializeField]
    private Button quitButton;

    [Tooltip("Tutorial text reference")]
    [SerializeField]
    private TextMeshProUGUI tutorialText;

    [Tooltip("Determines how long the tutorial text stays on screen")]
    [SerializeField]
    private float tutorialTextDuration;

    [Tooltip("Warning text reference")]
    [SerializeField]
    private TextMeshProUGUI warningText;

    [Tooltip("Lose text reference")]
    [SerializeField]
    private TextMeshProUGUI loseText;

    [Tooltip("Win text reference")]
    [SerializeField]
    private TextMeshProUGUI winText;

    [Tooltip("Player animator reference")]
    [SerializeField]
    private Animator animator;

    [HideInInspector]
    public static bool deathAnimCompleted;

    [Tooltip("Planet's Transform reference")]
    [SerializeField]
    private Transform planet;

    [Tooltip("Player's Transform reference")]
    [SerializeField]
    private Transform player;


    void Start()
    {
        // Set time line to normal
        Time.timeScale = 1f;

        deathAnimCompleted = false;

        // Deactivate tutorial text after two seconds
        StartCoroutine(DisableTutorialTextWithDelay());
    }
    
    void Update()
    {
        if(Player.hasDied)
        {
            if(deathAnimCompleted)
                Time.timeScale = 0f;  // Stop time line


            // Activate lose text
            loseText.gameObject.SetActive(true);

            // Activate retry button
            retryButton.gameObject.SetActive(true);

            // Activate quit button
            quitButton.gameObject.SetActive(true);

            // Play death animation
            animator.SetTrigger("die");
        }
        else if (Player.hasWon)
        {
            // Stop time line
            Time.timeScale = 0f;

            // Activate win text
            winText.gameObject.SetActive(true);

            // Activate retry button
            retryButton.gameObject.SetActive(true);

            // Activate quit button
            quitButton.gameObject.SetActive(true);
        }



        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }

            else
                Pause();
        }

        // If distance between player and planet is greater than 80
        if(Vector3.Distance(player.position,planet.position) > 80)
        {
            warningText.color = Color.white;
            warningText.gameObject.SetActive(true);
        }

        // If distance between player and planet is greater than 95
        if (Vector3.Distance(player.position, planet.position) > 95)
        {
            warningText.color = Color.red;
            warningText.gameObject.SetActive(true);
        }

        // If distance between player and planet is greater than 115
        if (Vector3.Distance(player.position, planet.position) > 115)
        {
            // LOST
            Player.hasDied = true;
            warningText.gameObject.SetActive(false);
        }

        // If distance between player and planet is smalle than 80
        if (Vector3.Distance(player.position, planet.position) < 80)
            warningText.gameObject.SetActive(false);
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        // Return time line to normal
        Time.timeScale = 1f;
        gameIsPaused = false;

        // Disable pause menu
        pauseMenuUI.SetActive(false);
    }

    private void Pause()
    {
        // Stop time line
        Time.timeScale = 0f;
        gameIsPaused = true;

        // Activate pause menu
        pauseMenuUI.SetActive(true);
    }

    private IEnumerator  DisableTutorialTextWithDelay()
    {
        yield return new WaitForSeconds(tutorialTextDuration);
        tutorialText.gameObject.SetActive(false);
    }
}
