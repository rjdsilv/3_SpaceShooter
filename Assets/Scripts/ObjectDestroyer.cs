using UnityEngine;

/// <summary>
/// Class responsible for destroying objects that leaves the game area.
/// </summary>
public class ObjectDestroyer : MonoBehaviour
{
    /// <summary>
    /// Destroys any object that leaves the game area.
    /// </summary>
    /// <param name="collision">The object to be destroyed</param>
    void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
