using System;
using UnityEngine;

[Serializable]
public class SpawnProperties
{
    // Script public variables.
    public float waitSeconds; // The number of seconds to wait after the level started to spawn.
    public float xPosition;   // The x position to spawn.
    public float yPosition;   // The y position to spawn.
    public GameObject enemy;  // The enemy to spawn.

    // Script private variables.
    private bool hasSpawned = false;

    /// <summary>
    /// Indicates if the enemy has already spawned.
    /// </summary>
    /// <returns>true if the enemy has spawned. false otherwise.</returns>
    public bool HasSpawned()
    {
        return hasSpawned;
    }

    /// <summary>
    /// Marks the enemy as already spawned.
    /// </summary>
    public void MarkAsSpawned()
    {
        hasSpawned = true;
    }
}
