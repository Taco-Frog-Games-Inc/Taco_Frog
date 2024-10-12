using UnityEngine;
/*
 * Source File Name: EnemyUtilities.cs
 * Author Name: Audrey Bernier Larose
 * Student Number: 301166198
 * Creation Date: October 2nd, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: October 2nd, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script provides utilities for an enemy controller.
 * 
 * Revision History:
 *      -> October 2nd, 2024:
 *          -Created this script and fully implemented it.
 */
public static class EnemyUtilities
{
    /// <summary>
    /// Checks if the conditions to sense another game object is met.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="other"></param>
    /// <param name="cosOtherFOVOver2InRad"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public static bool SenseOther(GameObject obj, GameObject other, float cosOtherFOVOver2InRad, float distance)
    {
        return OtherInFront(obj, other, cosOtherFOVOver2InRad) && OtherCloseEnough(distance, obj, other);
    }

    /// <summary>
    /// Checks if the other game object is positioned in front.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="other"></param>
    /// <param name="cosOtherFOVOver2InRad"></param>
    /// <returns></returns>
    private static bool OtherInFront(GameObject obj, GameObject other, float cosOtherFOVOver2InRad)
    {
        Vector3 otherHeading = (other.transform.position - obj.transform.position).normalized;
        float cosAngle = Vector3.Dot(otherHeading, obj.transform.forward);

        return cosAngle > cosOtherFOVOver2InRad;
    }

    /// <summary>
    /// Checks if the other game object is close enough.
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="obj"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    private static bool OtherCloseEnough(float distance, GameObject obj, GameObject other)
    {
        return Vector3.Distance(obj.transform.position, other.transform.position) <= distance;
    }
}
