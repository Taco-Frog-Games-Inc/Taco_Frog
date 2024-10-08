
using UnityEngine;

public abstract class SpawnManager: MonoBehaviour
{
    protected readonly GameObject _prefab;
    protected readonly LayerMask _mask;
    protected readonly float _height = 10f;

    protected Vector3 _mapSize;
    
   public SpawnManager(GameObject prefab, LayerMask mask, Vector3 mapSize)
   {
        _prefab = prefab;
        _mask = mask;
       
        _mapSize = mapSize;
   }

   public   virtual void SpawnGameObjects(int entityCount)
   {
          int initalEnemy = 0;

        while (initalEnemy < entityCount)
        {
           
            Vector3 randomPosition = new Vector3(
                Random.Range(-_mapSize.x / 2, _mapSize.x / 2),
                _height,  
                Random.Range(-_mapSize.z / 2, _mapSize.z / 2)
            );

           
            Ray ray = new Ray(randomPosition, Vector3.down);
            RaycastHit hit;

            
            if (Physics.Raycast(ray, out hit, _height, _mask))
            {
               
                Vector3 spawnPosition = hit.point + new Vector3(0, 0f, 0);
                Instantiate(_prefab, spawnPosition, Quaternion.identity);
                initalEnemy++;
            }
        }
   }
}
