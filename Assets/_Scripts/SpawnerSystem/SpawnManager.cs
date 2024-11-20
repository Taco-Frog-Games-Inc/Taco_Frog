using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public abstract class SpawnManager
{
    protected readonly GameObject _prefab;
   // protected readonly LayerMask _mask;
    protected readonly float _height = 10f;

    private float minSpawnDistance = 10f;
    protected Vector3 _mapSize;

    protected bool isItem;

    private List<Vector3> spawnPositions = new List<Vector3>();
    
    public SpawnManager(){}
   public SpawnManager(GameObject prefab,  Vector3 mapSize, bool item)
   {
        _prefab = prefab;
       
       
        _mapSize = mapSize;
        isItem = item;
   }

   public virtual void SpawnGameObjects(int itemCount)

    {
       

       
        SpawnLogic(itemCount);
    }

    private void SpawnLogic(int itemCount)
    {
        int initialCount = 0;
        Debug.Log("SpawnGameObjects function is called!");

        while (initialCount < itemCount)
        {
            // Generate a random position within map bounds
            Vector3 randomPosition = new Vector3(
                Random.Range(-_mapSize.x / 2f, _mapSize.x / 2f),
                _height,
                Random.Range(-_mapSize.z / 2f, _mapSize.z / 2f)
            );

            // Project the random position onto the NavMesh
            initialCount = SpawnByGroundDetection(initialCount, randomPosition);
        } 
    }

    private int SpawnByGroundDetection(int initialCount, Vector3 randomPosition)
    {
        _mapSize.x = 10f;
        _mapSize.z = 10f;
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit navHit, Mathf.Max(_mapSize.x, _mapSize.z) / 2f, NavMesh.AllAreas))
        {
            Vector3 spawnPosition = navHit.position + (isItem ? new Vector3(0, 1f, 0) : Vector3.zero);

            // Check distance from other spawn positions
            bool isFarEnough = true;
            foreach (Vector3 pos in spawnPositions)
            {
                if (Vector3.Distance(spawnPosition, pos) < minSpawnDistance)
                {
                    isFarEnough = false;
                    break;
                }
            }

            // Spawn only if position is far enough from others
            if (isFarEnough)
            {
                MonoBehaviour.Instantiate(_prefab, spawnPosition, Quaternion.identity);
                spawnPositions.Add(spawnPosition);
                initialCount++;
                Debug.Log($"Spawned object {initialCount} at: {spawnPosition}");
            }
        }
        else
        {
            Debug.LogWarning("NavMesh.SamplePosition did not find a valid position.");
        }

        return initialCount;
    }

    
   

    public   virtual void SpawnGameItems(int entityCount)
   {
         
    
   }
}
