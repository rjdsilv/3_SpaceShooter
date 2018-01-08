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
    public Text bossLifeText;

    public GameObject ship;
    public SpawnProperties[] spawnProperties;
    public BackgroundScroller[] scroller;

    // Script private variables.
    private bool gameOver;
    private bool restart;
    private bool reloadScene = false;
    private bool startGame = false;
    private float startTime;
    private AudioSource gameMusic;
    private AudioSource gameOverMusic;
    private AudioSource gameBossFightMusic;

    /// <summary>
    /// Method responible for startup.
    /// </summary>
    void Start()
    {
        var audioSources = GetComponents<AudioSource>();
        gameMusic = audioSources[0];
        gameOverMusic = audioSources[1];
        gameBossFightMusic = audioSources[2];
        gameMusic.Play();
        gameOverMusic.Stop();
        InitializeGameText();
        StartCoroutine(PrepareLoad());
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // If the player dies, reloads the scene from the beginning.
        if (gameOver)
        {
            Restart();
        }
        else
        {
            if (reloadScene)
            {
                StartCoroutine(ReloadScene(3));
                reloadScene = false;
            }
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
        if (!gameOver && startGame)
        {
            // Spawn the enemies.
            for (int i = 0; i < spawnProperties.Length; i++)
            {
                EnemyController enemyController = spawnProperties[i].controller;

                if ((Time.time - startTime) > spawnProperties[i].waitSeconds && !spawnProperties[i].HasSpawned())
                {
                    StartCoroutine(enemyController.Spawn(spawnProperties[i].xPosition, spawnProperties[i].yPosition));
                    spawnProperties[i].MarkAsSpawned();
                    if (enemyController.isBoss)
                    {
                        StartCoroutine(PlayBossMusic());
                        UpdateBossLife(enemyController.enemyLife);
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
        gameMusic.Stop();
        gameOverMusic.Play();
    }

    public void Win()
    {
        gameOver = true;
        gameOverText.text = "You Won!!!";
    }

    private IEnumerator PlayBossMusic()
    {
        while (gameMusic.volume > 0)
        {
            gameMusic.volume -= 0.05f;
            yield return new WaitForSeconds(0.5f);
        }

        gameMusic.Stop();
        yield return new WaitForSeconds(2);
        gameBossFightMusic.volume = 0;
        gameBossFightMusic.Play();

        while (gameBossFightMusic.volume < 0.3f)
        {
            gameBossFightMusic.volume += 0.05f;
            yield return new WaitForSeconds(0.5f);
        }
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

    public void UpdateBossLife(int value)
    {
        bossLifeText.text = string.Format("Boss Life: {0}", value);
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
        bossLifeText.text = "";
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
