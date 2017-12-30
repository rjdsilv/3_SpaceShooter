using UnityEngine;

/// <summary>
/// Class responsible for destroing a game object. It will be mostly used in animations.
/// </summary>
public class Destroy : MonoBehaviour
{
    /// <summary>
    /// Method to destroy the attached gameobject.
    /// </summary>
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
