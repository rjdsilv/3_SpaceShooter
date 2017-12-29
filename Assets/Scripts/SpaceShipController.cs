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
    private float shotTimer = 0;

	// Use this for initialization
	void Start ()
    {
        rBody = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
        shotTimer += Time.deltaTime;

        if (Input.GetButton("Fire") && shotTimer > shotInterval)
        {
            Instantiate(laser, laserSpawner.position, laserSpawner.rotation);
            shotTimer = 0;
        }
    }

    // Like update but used with physics
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
