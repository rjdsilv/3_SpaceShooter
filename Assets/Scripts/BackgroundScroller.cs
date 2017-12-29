using UnityEngine;

/// <summary>
/// Class that will be responsible for scrolling the background to left to give te impression that
/// the ship is actualy moving
/// 
/// Author : Rodrigo Januario da Silva
/// Version: 0.0.1
/// </summary>
public class BackgroundScroller : MonoBehaviour
{
    // Script public variables.
    public float scrollSpeed; // The background move speed.
    public float tileSize;  // The background tile size.

    // Script private variable.
    private Vector3 startPosition;

	// Use this for initialization
	void Start ()
    {
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSize);
        transform.position = startPosition + Vector3.right * newPosition;
	}
}
