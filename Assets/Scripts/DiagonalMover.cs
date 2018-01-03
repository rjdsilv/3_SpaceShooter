using UnityEngine;

public class DiagonalMover : MonoBehaviour
{
    // Script public variables.
    public float xDirection;
    public float yDirection;
    public float speed;

    // Script private variable
    private Rigidbody2D rBody;
    private float startTime;
    private bool rotated = false;

    // Use this for initialization
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rBody.AddForce(new Vector2(xDirection, yDirection) * speed);

        if (rBody.velocity.magnitude > 0 && !rotated)
        {
            if (rBody.velocity.y > 0)
            {
                transform.Rotate(Vector3.back, Vector3.Angle(Vector3.right, new Vector3(rBody.velocity.x, rBody.velocity.y, 0)));
            }
            else
            {
                transform.Rotate(Vector3.back, Vector3.Angle(Vector3.left, new Vector3(rBody.velocity.x, rBody.velocity.y, 0)));
            }
            rotated = true;
        }
    }
}
