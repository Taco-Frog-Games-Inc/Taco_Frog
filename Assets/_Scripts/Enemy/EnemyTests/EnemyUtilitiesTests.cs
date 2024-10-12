using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;


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

        Object.Destroy(obj);
        Object.Destroy(other);
    }

    [Test]
    public void EnemyNavMeshAgent_Speed()
    {
        GameObject _enemy = new("Enemy");
        _enemy.AddComponent<NavMeshAgent>();
        ShortRangeEnemy enemy = _enemy.AddComponent<ShortRangeEnemy>();
        NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();

        float initialSpeed = agent.speed;
        float increasedSpeed = agent.speed *= 1.2f;

        Assert.Greater(increasedSpeed, initialSpeed);

        Object.Destroy(enemy);       
    }


    [Test]
    public void Enemy_Health()
    {
        GameObject _enemy = new("Enemy");
        ShortRangeEnemy enemy = _enemy.AddComponent<ShortRangeEnemy>();
        enemy.Health = 100;

        enemy.TakeDamage(50);

        Assert.Less(enemy.Health, 100);
        
        Object.Destroy(enemy);
    }
}
