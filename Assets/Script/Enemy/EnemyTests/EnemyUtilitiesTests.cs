using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class EnemyUtilitiesTests
{
    [Test]
    public void SenseOther_ReturnsTrue_WhenOtherIsInCloseEnoughCutoffRange()
    {        
        GameObject obj = new("Obj");
        GameObject other = new("Other");
        obj.transform.position = Vector3.zero;
        other.transform.position = new Vector3(0, 0, 1); // Positioned in front

        float EnemyFOV = 89f;
        float cosFOV = Mathf.Cos(EnemyFOV / 2f * Mathf.Deg2Rad);
        float closeEnoughSenseCutoff = 45f;

        bool result = EnemyUtilities.SenseOther(obj, other, cosFOV, closeEnoughSenseCutoff);

        Assert.IsTrue(result);
    }

}
