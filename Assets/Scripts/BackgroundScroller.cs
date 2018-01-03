using UnityEngine;

/// <summary>
/// Class that will be responsible for scrolling the background to left to give te impression that
/// the ship is actualy moving
/// </summary>
public class BackgroundScroller : MonoBehaviour
{
    // Script public variables.
    public float scrollSpeed; // The background move speed.
    public float tileSize;    // The background tile size.

    // Script private variable.
    private Vector3 startPosition;
    private bool move = true;

	/// <summary>
    /// Initializes the scroller.
    /// </summary>
	void Start ()
    {
        startPosition = transform.position;
	}
	
	/// <summary>
    /// Performs the calculation for scrolling the background and respawning it when it leaves the game area.
    /// </summary>
	void FixedUpdate ()
    {
        if (move)
        {
            float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSize);
            transform.position = startPosition + Vector3.right * newPosition;
        }
	}

    public void StopMoving()
    {
        move = false;
    }

    public void StartMoving()
    {
        move = true;
    }
}
