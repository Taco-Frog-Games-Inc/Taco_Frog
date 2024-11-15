using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnManager: MonoBehaviour
{
    protected readonly GameObject _prefab;
    protected readonly LayerMask _mask;
    protected readonly float _height = 10f;

    private float minSpawnDistance = 10f;
    protected Vector3 _mapSize;

    protected bool isItem;

    private List<Vector3> spawnPositions = new List<Vector3>();
    
    public SpawnManager(){}
   public SpawnManager(GameObject prefab, LayerMask mask, Vector3 mapSize, bool item)
   {
        _prefab = prefab;
        _mask = mask;
       
        _mapSize = mapSize;
        isItem = item;
   }

   public   virtual void SpawnGameObjects(int entityCount)
   {
          int initalEnemy = 0;
          Debug.Log("The funciton is called!");

        while (initalEnemy < entityCount)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-_mapSize.x / 0.5f, _mapSize.x / 0.5f),
                _height,
                Random.Range(-_mapSize.z / 0.5f, _mapSize.z / 0.5f)
            );

            Ray ray = new Ray(randomPosition, Vector3.down);
            RaycastHit hit;

            // Check for ground hit
            if (Physics.Raycast(ray, out hit, _height, _mask))
            {
                Vector3 spawnPosition = hit.point + (isItem ? new Vector3(0, 0.25f, 0) : new Vector3(0, 0, 0));

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
                    initalEnemy++;
                }
            }
        }
    
   }

    public   virtual void SpawnGameItems(int entityCount)
   {
         
    
   }
}
