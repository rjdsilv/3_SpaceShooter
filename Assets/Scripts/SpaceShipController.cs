using UnityEngine;

/// <summary>
/// Class responsible for controlling the space ship.
/// </summary>
public class SpaceShipController : MonoBehaviour
{
    // Script public variables.
    public float speed;
    public float shotInterval;
    public Boundary boundary;
    public Transform laserSpawner;
    public GameObject laser;

    // Script private variables.
    private Rigidbody2D rBody;
    private AudioSource laserAudio;
    private float shotTimer = 0;

	// Use this for initialization
	void Start ()
    {
        rBody = GetComponent<Rigidbody2D>();
        laserAudio = GetComponent<AudioSource>();
	}

    /// <summary>
    /// Makes the calculation for the fire time and spawns the laser being shot.
    /// </summary>
    void Update()
    {
        shotTimer += Time.deltaTime;

        if (Input.GetButton("Fire") && shotTimer > shotInterval)
        {
            Instantiate(laser, laserSpawner.position, laserSpawner.rotation);
            laserAudio.Play();
            shotTimer = 0;
        }
    }

    /// <summary>
    /// Performs the calculation for the ship movement and estabilishes the ship boundaries.
    /// </summary>
    void FixedUpdate ()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rBody.velocity = movement * speed;
        rBody.position = new Vector2(
            Mathf.Clamp(rBody.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rBody.position.y, boundary.yMin, boundary.yMax)
        );
	}
}
