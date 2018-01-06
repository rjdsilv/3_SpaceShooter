using UnityEngine;

/// <summary>
/// Class responsible for destroying game objects colliding and render the explosion.
/// </summary>
public class DestroyOnImpact : MonoBehaviour
{
    // Script public variables
    public GameObject explosionPlayer;
    public GameObject explosionEnemy;

    // Script private variables.
    private GameController gameController;

    void Start()
    {
        GameObject controllerObject = GameObject.FindWithTag("GameController");
        if (null != controllerObject)
        {
            gameController = controllerObject.GetComponent<GameController>();
        }
    }

    /// <summary>
    /// This method will detect collision between enemy and 
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPlayerObject(other.tag) && IsEnemyObject(tag))
        {
            // Only instatiate the explosion for the player and not for the laser.
            if (IsPlayerShip(other.tag))
            {
                Instantiate(explosionPlayer, other.gameObject.transform.position, other.gameObject.transform.rotation);
                gameController.DecreaseLife();

                // Controls the game over.
                if (!gameController.HasLives())
                {
                    gameController.GameOver();
                }
            }

            // Only instantiate the explosion for the enemy and not for the shot.
            if (IsEnemyShip(tag))
            {
                Instantiate(explosionEnemy, gameObject.transform.position, gameObject.transform.rotation);
            }

            if (!(IsLaser(other.tag) && IsShot(tag)))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
        else if (IsShield(tag) && IsPlayerObject(other.tag))
        {
            // Only instatiate the explosion for the player and not for the laser.
            if (IsPlayerShip(other.tag))
            {
                Instantiate(explosionPlayer, other.gameObject.transform.position, other.gameObject.transform.rotation);
                gameController.DecreaseLife();

                // Controls the game over.
                if (!gameController.HasLives())
                {
                    gameController.GameOver();
                }
            }

            Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// Indicates if the object colliding is a laser.
    /// </summary>
    /// <param name="tag">The object's tag.</param>
    /// <returns>true if the object is the laser. false otherwise.</returns>
    private bool IsLaser(string tag)
    {
        return "Laser" == tag;
    }

    /// <summary>
    /// Indicates if the object colliding is the player.
    /// </summary>
    /// <param name="tag">The object's tag.</param>
    /// <returns>true if the object is the player. false otherwise.</returns>
    private bool IsPlayerShip(string tag)
    {
        return "Player" == tag;
    }

    /// <summary>
    /// Indicates if the object colliding is a player object which means the ship or the laser.
    /// </summary>
    /// <param name="tag">The object's tag.</param>
    /// <returns>true if the object is the ship or laser. false otherwise.</returns>
    private bool IsPlayerObject(string tag)
    {
        return IsLaser(tag) || IsPlayerShip(tag);
    }

    /// <summary>
    /// Indicates if the object colliding is the enemy ship.
    /// </summary>
    /// <param name="tag">The object's tag.</param>
    /// <returns>true if the object is the enemy ship. false otherwise.</returns>
    private bool IsEnemyShip(string tag)
    {
        return "Enemy" == tag;
    }

    /// <summary>
    /// Indicates if the object colliding is the enemy shot.
    /// </summary>
    /// <param name="tag">The object's tag.</param>
    /// <returns>true if the object is the enemy shot. false otherwise.</returns>
    private bool IsShot(string tag)
    {
        return "Shot" == tag;
    }

    /// <summary>
    /// Indicates if the object colliding is an enemy object which means an enemy ship or an enemy shot.
    /// </summary>
    /// <param name="tag">The object's tag.</param>
    /// <returns>true if the object is the enemy ship or the enemy shot. false otherwise.</returns>
    private bool IsEnemyObject(string tag)
    {
        return IsEnemyShip(tag) || IsShot(tag);
    }

    /// <summary>
    /// Indicates if the object colliding is shield.
    /// </summary>
    /// <param name="tag">The object's tag.</param>
    /// <returns>true if the object is the enemy ship or the enemy shot. false otherwise.</returns>
    private bool IsShield(string tag)
    {
        return "Shield" == tag;
    }
}
