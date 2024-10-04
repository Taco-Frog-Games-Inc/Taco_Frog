using UnityEngine;

public static class EnemyUtilities
{
    public static bool SenseOther(GameObject obj, GameObject other, float cosOtherFOVOver2InRad, float distance)
    {
        return OtherInFront(obj, other, cosOtherFOVOver2InRad) && OtherCloseEnough(distance, obj, other);
    }

    public static bool OtherInFront(GameObject obj, GameObject other, float cosOtherFOVOver2InRad)
    {
        Vector3 otherHeading = (other.transform.position - obj.transform.position).normalized;
        float cosAngle = Vector3.Dot(otherHeading, obj.transform.forward);

        return cosAngle > cosOtherFOVOver2InRad;
    }

    public static bool OtherCloseEnough(float distance, GameObject obj, GameObject other)
    {
        return Vector3.Distance(obj.transform.position, other.transform.position) <= distance;
    }
}
