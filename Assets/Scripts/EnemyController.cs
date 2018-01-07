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
    public int destroyScore;              // The number of points player will earn after destroying the enemy.
    public int formationSize;             // The number of enemies in the formation.
    public int formationColumns;          // The number of columns in the TRIANGLE formation.
    public int enemyLife;                 // The ammount of life the enemy has.
    public bool spawnInFormations;        // Indicates if the enemy will spawn in formation.
    public bool shotBack;                 // Indicates if the enemy shots back at the player.
    public bool isBoss;                   // Indicates if the enemy is a boss;
    public float nextEnemyWait;           // The wait for spawning the next enemy in the formation.
    public float shotInterval;            // The enemy shot interval, if it shots back.
    public float shotBackSpeed;           // The speed that the enemy shot will travel on the screen.
    public FormationType formationType;   // The enemy formation type, if it spawns in formation.
    public GameObject enemyObject;        // The enemy object to be controlled.
    public GameObject enemyShot;          // The enemy shot object that will be used.

    // Script private variables.
    private static bool hasShield = true;

    /// <summary>
    /// Spawns the enemies based on the properties given by the programmer on unit's screen.
    /// </summary>
    /// <returns>The IEnumerator so this method can be used as a coroutine.</returns>
    public IEnumerator Spawn(float xPosition, float yPosition)
    {
        hasShield = true;
        formationSize = FormationType.LINE == formationType ? formationSize : CalculateFormationSize(formationColumns);
        GameObject[] enemies = spawnInFormations ? new GameObject[formationSize] : new GameObject[1];

        // Spawn the enemies
        yield return SpawnEnemies(enemies, xPosition, yPosition);

        // Makes the enemies shot back.
        yield return ShotBack(enemies);
    }

    private static bool HasShield(GameObject enemy)
    {
        if (hasShield)
        {
            foreach (Transform spawner in enemy.transform)
            {
                if (spawner.tag == "Shield")
                {
                    return true;
                }
            }

            hasShield = false;
        }

        return hasShield;
    }

    private IEnumerator SpawnEnemies(GameObject[] enemies, float xPosition, float yPosition)
    {
        if (spawnInFormations)
        {
            if (FormationType.LINE == formationType)
            {
                for (int i = 0; i < formationSize; i++)
                {
                    enemies[i] = Instantiate(enemyObject, new Vector3(xPosition, yPosition), enemyObject.transform.rotation);
                    yield return new WaitForSeconds(nextEnemyWait);
                }
            }
            else if (FormationType.TRIANGLE == formationType)
            {
                float currYPos = yPosition;
                float maxY = -100;

                for (int i = 0, k = 0; i < formationColumns; i++)
                {
                    yield return new WaitForSeconds(nextEnemyWait);
                    for (int j = 0; j <= i; j++, k++)
                    {
                        enemies[k] = Instantiate(enemyObject, new Vector3(xPosition, currYPos), enemyObject.transform.rotation);
                        float enemyHeight = enemies[i + j].GetComponent<Collider2D>().bounds.size.y;
                        enemyHeight += enemyHeight / 2;
                        if (maxY < (currYPos + enemyHeight / 2))
                        {
                            maxY = currYPos + enemyHeight / 2;
                        }
                        currYPos -= enemyHeight;
                    }
                    currYPos = maxY;
                }
            }
        }
        else
        {
            enemies[0] = Instantiate(enemyObject, new Vector3(xPosition, yPosition), enemyObject.transform.rotation);
        }
    }

    private IEnumerator ShotBack(GameObject[] enemies)
    {
        if (shotBack)
        {
            // Gets the player on the scene.
            GameObject ship = GameObject.FindGameObjectWithTag("Player");

            while (AreEnemiesAlive(enemies))
            {
                foreach (GameObject enemy in enemies)
                {
                    if (!isBoss)
                    {
                        EnemyShot(ship, enemy);

                        // Waits just for alive enemies.
                        if (null != enemy)
                        {
                            yield return new WaitForSeconds(shotInterval);
                        }
                    }
                    else
                    {
                        // The Boss shots only after his shield is down.
                        if (!HasShield(enemy))
                        {
                            yield return BossShot(ship, enemy, 0, 4, false);
                            yield return new WaitForSeconds(shotInterval);
                            yield return BossShot(ship, enemy, 4, 10, false);
                            yield return new WaitForSeconds(shotInterval);
                            yield return BossShot(ship, enemy, 0, 10, true);
                            yield return new WaitForSeconds(shotInterval);
                            yield return BossShot(ship, enemy, 0, 10, false);
                            yield return new WaitForSeconds(shotInterval);
                        }
                        else
                        {
                            yield return new WaitForSeconds(shotInterval / 2);
                        }
                    }
                }
            }
        }
    }

    private IEnumerator BossShot(GameObject ship, GameObject enemy, int fromWeapon, int toWeapon, bool waitNextShot)
    {
        for (int i = fromWeapon; i < toWeapon; i++)
        {
            try
            {
                if (null != ship)
                {
                    Transform currChild = enemy.transform.GetChild(i);
                    if (currChild.tag != "Shield")
                    {
                        Vector3 shotDirection = ship.transform.position - currChild.position;
                        GameObject shot = Instantiate(enemyShot, currChild.position, Quaternion.identity);
                        shot.GetComponent<Rigidbody2D>().velocity = shotDirection.normalized * shotBackSpeed;
                    }
                }
            }
            catch (Exception)
            {
            }

            if (waitNextShot)
            {
                yield return new WaitForSeconds(shotInterval / 2);
            }
        }
    }

    private void EnemyShot(GameObject ship, GameObject enemy)
    {
        try
        {
            // The player is still alive?
            if (null != ship)
            {
                Vector3 shotDirection = ship.transform.position - enemy.transform.position;
                GameObject shot = Instantiate(enemyShot, enemy.transform.GetChild(0).position, Quaternion.identity);
                shot.GetComponent<Rigidbody2D>().velocity = shotDirection.normalized * shotBackSpeed;
            }
        }
        catch (Exception)
        {
        }
    }

    private bool AreEnemiesAlive(GameObject[] enemies)
    {
        foreach (GameObject enemy in enemies)
        {
            if (null != enemy)
            {
                return true;
            }
        }

        return false;
    }

    private int CalculateFormationSize(int formationColumns)
    {
        if (formationColumns == 1)
        {
            return formationColumns;
        }

        return CalculateFormationSize(formationColumns - 1) + formationColumns;
    }
}
