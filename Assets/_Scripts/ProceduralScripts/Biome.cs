using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Source File Name: Biome.cs
 * Author Name: Audrey Bernier Larose
 * Student Number: 301166198
 * Creation Date: October 12th, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: October 14th, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script handles the biomes and their spawnability properties
 * 
 * Revision History:
 *      -> October 12th, 2024:
 *          -Created this script and fully implemented it.
 *      -> October 14th, 2024:
 *          -Adjusted for multiplayer
 */
public class Biome : MonoBehaviour
{
    private GameObject spawnPoint;

    [Header("SpawnObjects")]
    [SerializeField] private List<GameObject> enemyTypes = new();
    [SerializeField] private GameObject spawnerPublisher;

    private GameObject path;

    private GameObject[] enemyTypesArray;

    private bool isMapGeneratorComplete = false;
    private bool isInitialed = false;

    /// <summary>
    /// Initializes this script's variables and subscribes to the spawner publisher
    /// </summary>
    private void OnEnable()
    {
        Subscribe();
        path = GameObject.Find("Path");
        spawnPoint = gameObject.transform.GetChild(0).gameObject;
        Vector3 worldPosition = spawnPoint.transform.position;        

        GameObject waypoint = Instantiate(spawnPoint, worldPosition, Quaternion.identity);
        waypoint.transform.SetParent(path.transform, false);
    }

    private void Update()
    {
        if (IsReadyForCoroutine()) {
            StartCoroutine(SpawnAgentsAfterBake());
            isInitialed = true;
        }
    }

    /// <summary>
    /// Checks if all conditions are met before spawning enemies
    /// </summary>
    /// <returns></returns>
    private bool IsReadyForCoroutine() {
        return gameObject.name.Contains("Grass_1") && 
               !isInitialed && 
               isMapGeneratorComplete && 
               NavMeshManager.isInitialize && 
               GameObject.Find("PlayerContainer(Clone)") != null;
    }

    /// <summary>
    /// Provides a short delay between the time the map has finished baking and the first enemy spawn
    /// </summary>
    /// <returns></returns>
    private System.Collections.IEnumerator SpawnAgentsAfterBake()
    {        
        yield return null;    
        if(!MapGenerator.hasOneEnemy)
        OnSpawning();
    }

    /// <summary>
    /// Subscribes to the spawner publisher
    /// </summary>
    public void Subscribe() { spawnerPublisher.GetComponent<SpawnerPublisher>().SpawningEvent += MapGeneratorComplete; }

    /// <summary>
    /// Called once the spawner publisher emits
    /// </summary>
    private void MapGeneratorComplete() { isMapGeneratorComplete = true; }

    /// <summary>
    /// Spawns an enemy on the biome if the random number is greater than 90.
    /// This provides a 10% chance to spawn an enemy. 
    /// </summary>
    private void OnSpawning()
    {
        float randomNumber = Random.Range(0f, 101f);
        if (randomNumber > 90)
        {            
            enemyTypesArray = enemyTypes.ToArray();
            GameObject enemy = enemyTypesArray[0];
            enemy.GetComponent<EnemyController>().path = path;
            
            if (path.transform.childCount > 0)                
                InstantiateEnemyOnTop(enemy, gameObject);                                                                                                         
        }
    }

    /// <summary>
    /// Ensures the position of the enemy is on a navmesh surface
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="platform"></param>
    private void InstantiateEnemyOnTop(GameObject enemy, GameObject platform)
    {
        if (platform != null && enemy != null)
        {
            Vector3 bioimePosition = platform.transform.position;

            float biomeHeight = platform.transform.localScale.y;
            float enemyHeight = enemy.transform.localScale.y;

            Vector3 enemyPosition = new(
                bioimePosition.x,
                bioimePosition.y + (biomeHeight / 2) + (enemyHeight / 2),
                bioimePosition.z
            );

            if (NavMesh.SamplePosition(enemyPosition, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
                Instantiate(enemy, hit.position, Quaternion.identity);
        }        
    }
}
