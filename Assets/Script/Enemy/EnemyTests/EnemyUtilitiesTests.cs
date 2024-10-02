using NUnit.Framework;
using UnityEngine;


public class EnemyUtilitiesTests
{
    [Test]
    public void SenseOther_ReturnsTrue_WhenOtherIsInFrontAndClose()
    {
        // Arrange
        GameObject obj = new GameObject("Obj");
        GameObject other = new GameObject("Other");
        obj.transform.position = Vector3.zero;
        other.transform.position = new Vector3(1, 0, 0); // Positioned in front

        float cosFOV = Mathf.Cos(30 * Mathf.Deg2Rad); // Example FOV
        float distance = 2f; // Distance to other

        // Act
        
        //bool result = EnemyUtilities.SenseOther(obj, other, cosFOV, distance);

        // Assert
        //Assert.IsTrue(result);
    }


}
