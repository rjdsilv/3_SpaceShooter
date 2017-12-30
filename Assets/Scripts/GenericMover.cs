using UnityEngine;

/// <summary>
/// Class responsible for moving the object horizontally.
/// </summary>
public class GenericMover : MonoBehaviour
{
    // Script public variables.
    public float speed;

	/// <summary>
    /// Makes the calculation to move the object horizontally.
    /// </summary>
	void Start ()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
	}
}
