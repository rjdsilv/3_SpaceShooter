using UnityEngine;

public class VectorUtils
{
    /// <summary>
    /// Calculates the angle between two given 3 dimension vectors.
    /// </summary>
    /// <param name="A">The first vector.</param>
    /// <param name="B">The second vector.</param>
    /// <returns>The angle between the given vectors.</returns>
    public static float AngleBetween(Vector3 A, Vector3 B)
    {
        return Mathf.Acos(Vector3.Dot(A, B) / (A.magnitude * B.magnitude));
    }
}
