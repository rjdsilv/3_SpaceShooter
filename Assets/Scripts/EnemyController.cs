using System;
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
    public float shotInterval;          // The enemy shot interval, if it shots back.
    public FormationType formationType; // The enemy formation type, if it spawns in formation.
}
