using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public  class SpawnController : MonoBehaviour, ISubscriber
{
   
    [Header("Item Spawn Station")]
    [SerializeField] private GameObject _prefab;
    [SerializeField] int _itemCount;
    protected readonly float _height = 10f;
    
    private float minSpawnDistance = 10f;
    protected Vector3 _mapSize;

    protected bool isItem;

    private List<Vector3> spawnPositions = new List<Vector3>();
    

    float height = 10f;
    float width = 10f;
   
   void Start()
   {
     SpawnGameObjects();
   }
   public  void SpawnGameObjects()

    {
       

       
        SpawnLogic();
    }

    private void SpawnLogic()
    {
        int initialCount = 0;
        Debug.Log("SpawnGameObjects function is called!");

        while (initialCount < _itemCount)
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
                Instantiate(_prefab, spawnPosition, Quaternion.identity);
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

    private int SpawnOnArea(int initialCount, Vector3 randomPosition)
    {
        _mapSize.x = 10f;
        _mapSize.z = 10f;
            Vector3 spawnPosition =  isItem ? new Vector3(0, 0.2f, 0) : Vector3.zero;

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
                Instantiate(_prefab, spawnPosition, Quaternion.identity);
                spawnPositions.Add(spawnPosition);
                initialCount++;
                Debug.Log($"Spawned object {initialCount} at: {spawnPosition}");
            }
       

        return initialCount;
    }

    public void Update_hl(float h, float l)
    {
        height = h;
        width = l;
       
    }
}
