using UnityEngine;

/// <summary>
/// Class responsible for making the object moving according to a SHM (Simple Harmonic Movement).
/// </summary>
public class SinusoidMover : MonoBehaviour
{
    // Script public variables.
    public float xSpeed;
    public float movementAmplitude;
    public float angularFrequency;
    public Boundary boundary;

    // Script private variable
    private Rigidbody2D rBody;
    private float startTime;

    // Use this for initialization
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        startTime = Time.time;
    }

    /// <summary>
    /// Makes the movement calculation based on the location formula of SHM (y = A * sin(wt)).
    /// </summary>
    void FixedUpdate()
    {
        rBody.velocity = new Vector2(-xSpeed, movementAmplitude * Mathf.Sin(angularFrequency * (Time.time - startTime)));
        rBody.position = new Vector2(
            Mathf.Clamp(rBody.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rBody.position.y, boundary.yMin, boundary.yMax)
        );
    }
}
