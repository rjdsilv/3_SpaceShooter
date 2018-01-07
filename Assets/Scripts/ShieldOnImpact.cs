using UnityEngine;

public class ShieldOnImpact : DestroyOnImpact
{
    /// <summary>
    /// This method will detect collision between enemy and 
    /// </summary>
    /// <param name="other"></param>
    override protected void OnTriggerEnter2D(Collider2D other)
    {
        if (IsShield(tag) && IsPlayerObject(other.tag))
        {
            shielded = true;
            ExplodePlayerShip(other);
            Destroy(other.gameObject);
        }
    }
}
