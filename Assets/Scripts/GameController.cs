using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class responsible for controlling the game logic.
/// </summary>
public class GameController : MonoBehaviour
{
    // Script public variables.
    public GameObject ship;
    public SpawnProperties[] spawnProperties;
    public BackgroundScroller[] scroller;

    // Script private variables.
    private bool gameOver;
    private bool restart;
    private bool reloadScene = false;
    private float startTime;
    
    /// <summary>
    /// Method responible for startup.
    /// </summary>
    void Start()
    {
        startTime = Time.time;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update ()
    {
        // If the player dies, reloads the scene from the beginning.
        if (!gameOver && reloadScene)
        {
            StartCoroutine(ReloadScene());
            reloadScene = false;
        }

        if (restart)
        {
            // Insert restart code here.
            restart = false;
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

    /// <summary>
    /// Indicates if the player still has lives to play.
    /// </summary>
    /// <returns>true if the player has lives. false otherwise.</returns>
    public bool HasLives()
    {
        return PlayerStats.lives > 0;
    }

    /// <summary>
    /// Set the game to be over, which means the player lost all lives.
    /// </summary>
    public void GameOver()
    {
        gameOver = true;
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
        PlayerStats.lives--;
        reloadScene = true;
    }

    /// <summary>
    /// Coroutine responsible for reloading the scene after the player dies.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        startTime = Time.time;
    }
}
