using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class responsible for controlling the game logic.
/// </summary>
public class GameController : MonoBehaviour
{
    // Script public variables.
    public Text scoreText;
    public Text livesText;
    public Text gameOverText;
    public Text restartText;
    public Text loadSceneText;

    public GameObject ship;
    public SpawnProperties[] spawnProperties;
    public BackgroundScroller[] scroller;

    // Script private variables.
    private bool gameOver;
    private bool restart;
    private bool reloadScene = false;
    private bool startGame = false;
    private float startTime;

    /// <summary>
    /// Method responible for startup.
    /// </summary>
    void Start()
    {
        InitializeGameText();
        StartCoroutine(PrepareLoad());
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // If the player dies, reloads the scene from the beginning.
        if (!gameOver && reloadScene)
        {
            StartCoroutine(ReloadScene(3));
            reloadScene = false;
        }

        if (restart)
        {
            if (string.IsNullOrEmpty(restartText.text))
            {
                restartText.text = "Press 'R' to Restart...";
            }

            // Insert restart code here.
            if (Input.GetKey(KeyCode.R))
            {
                PlayerStats.playerLives = 3;
                PlayerStats.playerScore = 0;
                restart = false;
                StartCoroutine(ReloadScene(2));
                gameOverText.text = "Restarting...";
            }
        }

        // Quit application.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// Method responsible for updating the physics calcularion every frame.
    /// </summary>
    void FixedUpdate()
    {
        if (gameOver)
        {
            Restart();
        }
        else
        {
            if (startGame)
            {
                // Spawn the enemies.
                for (int i = 0; i < spawnProperties.Length; i++)
                {
                    if ((Time.time - startTime) > spawnProperties[i].waitSeconds && !spawnProperties[i].HasSpawned())
                    {
                        StartCoroutine(spawnProperties[i].controller.Spawn(spawnProperties[i].xPosition, spawnProperties[i].yPosition));
                        spawnProperties[i].MarkAsSpawned();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Indicates if the player still has lives to play.
    /// </summary>
    /// <returns>true if the player has lives. false otherwise.</returns>
    public bool HasLives()
    {
        return PlayerStats.playerLives > 0;
    }

    /// <summary>
    /// Set the game to be over, which means the player lost all lives.
    /// </summary>
    public void GameOver()
    {
        gameOver = true;
        gameOverText.text = "Game Over";
    }

    public void Win()
    {
        gameOver = true;
        gameOverText.text = "You Won!!!";
    }

    private IEnumerator PerformGameOver()
    {
        yield return new WaitForSeconds(3);
    }

    /// <summary>
    /// Player asked for a game restart
    /// </summary>
    public void Restart()
    {
        restart = true;
    }

    /// <summary>
    /// Decreases the player lives.
    /// </summary>
    public void DecreaseLife()
    {
        PlayerStats.playerLives--;
        reloadScene = true;
        UpdateLives();
    }

    public void AddScore(int value)
    {
        PlayerStats.playerScore += value;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = string.Format("Score: {0}", PlayerStats.playerScore);
    }

    private void UpdateLives()
    {
        livesText.text = string.Format("Lives: {0}", PlayerStats.playerLives);
    }

    private void InitializeGameText()
    {
        gameOverText.text = "";
        restartText.text = "";
        UpdateScore();
        UpdateLives();
    }

    /// <summary>
    /// Coroutine responsible for reloading the scene after the player dies.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ReloadScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        startGame = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        startTime = Time.time;
    }

    private IEnumerator PrepareLoad()
    {
        loadSceneText.text = "Ready";
        yield return new WaitForSeconds(1);
        loadSceneText.text = "Set";
        yield return new WaitForSeconds(1.5f);
        loadSceneText.text = "GO!!!";
        yield return new WaitForSeconds(0.5f);
        startGame = true;
        loadSceneText.text = "";
        startTime = Time.time;
    }
}
