using UnityEngine;

/// <summary>
/// Class responsible for destroying objects that leaves the game area.
/// </summary>
public class ObjectDestroyer : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
