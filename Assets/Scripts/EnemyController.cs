using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Class responsible for storing the number of points that each enemy is worth when destroyed.
/// </summary>
[Serializable]
public class EnemyController : MonoBehaviour
{
    // Script public variables.
    public int destroyScore;            // The number of points player will earn after destroying the enemy.
    public int formationSize;           // The number of enemies in the formation.
    public bool spawnInFormations;      // Indicates if the enemy will spawn in formation.
    public bool shotBack;               // Indicates if the enemy shots back at the player.
    public float nextEnemyWait;         // The wait for spawning the next enemy in the formation.
    public float shotInterval;          // The enemy shot interval, if it shots back.
    public float shotBackSpeed;         // The speed that the enemy shot will travel on the screen.
    public FormationType formationType; // The enemy formation type, if it spawns in formation.
    public GameObject enemyObject;      // The enemy object to be controlled.
    public GameObject enemyShot;        // The enemy shot object that will be used.

    // Script private variables.
    private GameObject[] enemies;

    /// <summary>
    /// Spawns the enemies based on the properties given by the programmer on unit's screen.
    /// </summary>
    /// <returns>The IEnumerator so this method can be used as a coroutine.</returns>
    public IEnumerator Spawn(float xPosition, float yPosition)
    {
        enemies = new GameObject[formationSize];

        if (spawnInFormations)
        {
            for (int i = 0; i < formationSize; i++)
            {
                enemies[i] = Instantiate(enemyObject, new Vector3(xPosition, yPosition), enemyObject.transform.rotation);

                if (FormationType.LINE == formationType)
                {
                    yield return new WaitForSeconds(nextEnemyWait);
                }
            }
        }
        else
        {
            enemies[0] = Instantiate(enemyObject, new Vector3(xPosition, yPosition), enemyObject.transform.rotation);
        }
    }

    /// <summary>
    /// Makes the enemies shot back to the player's position.
    /// </summary>
    /// <returns>The IEnumerator so this method can be used as a coroutine.</returns>
    public IEnumerator ShotBack()
    {
        if (shotBack)
        {
            for (int i = 0; i < formationSize; i++)
            {
                yield return new WaitForSeconds(shotInterval);

                // Gets the player on the scene.
                GameObject ship = GameObject.FindGameObjectWithTag("Player");

                // The player is still alive?
                if (null != ship)
                {
                    Vector3 shotDirection = ship.transform.position - enemies[i].transform.position;
                    GameObject shot = Instantiate(enemyShot, enemies[i].transform.GetChild(0).position, Quaternion.identity);
                    shot.GetComponent<Rigidbody2D>().velocity = shotDirection.normalized * shotBackSpeed;
                }
            }
        }
    }
}
