using UnityEngine;

public class BossMover : MonoBehaviour
{
    public int xSpeed;
    public float stopPosition;

    // The boss Rigid Body
    private Rigidbody2D rBody;

    // Use this for initialization
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Makes the boss movement calculation.
    /// </summary>
    void FixedUpdate()
    {
        if (stopPosition < transform.position.x)
        {
            rBody.velocity = xSpeed * Vector2.left;
        }
        else
        {
            rBody.velocity = Vector2.zero;
            foreach(Transform child in transform)
            {
                if (child.tag == "Shield")
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
}
