using UnityEngine;

public class GenericMover : MonoBehaviour
{
    // Script public variables.
    public float speed;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
	}
}
