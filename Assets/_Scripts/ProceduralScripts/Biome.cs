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
 * Last Modified Date: December 5th, 2024
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
 *          -Created a occupiedLocations list to ensure spikes and items are not spawned on the same spot.
 *          -Created a way to spawn items, and hazards randomly on biomes.
 *      -> November 10th, 2024:
 *          -Changed the item variable to a list of items.
 *      -> December 5h, 2024:
 *          -Adjusted for difficulty
 */
public class Biome : MonoBehaviour
{
    private GameObject spawnPoint;

    [Header("Spawnable Objects")]
    [SerializeField] private List<GameObject> enemyTypes = new();
    [SerializeField] private GameObject spawnerPublisher;
    [SerializeField] private GameObject spawnerManager;
    [SerializeField] private GameObject spikes;
    [SerializeField] private List<GameObject> items;

    private List<Vector3> occupiedLocations = new();
    private GameObject path;    
    private GameObject[] enemyTypesArray;

    private bool isInitialed = false;

    /// <summary>
    /// Initializes this script's variables and subscribes to the spawner publisher
    /// </summary>
    private void OnEnable()
    {        
        //Subscribe();
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
        return (gameObject.name.Contains("Grass_1") || gameObject.name.Contains("Grass_1(Clone)")) &&
               !isInitialed &&
              
               NavMeshManager.isInitialize &&
               GameObject.Find("Player1") != null;
    }

    /// <summary>
    /// Provides a short delay between the time the map has finished baking and the first enemy spawn
    /// </summary>
    /// <returns></returns>
    private System.Collections.IEnumerator SpawnAgentsAfterBake()
    {        
        yield return null; 
        OnSpawning();
    }

    /// <summary>
    /// Subscribes to the spawner publisher
    /// </summary>
    //public void Subscribe() { spawnerPublisher.GetComponent<SpawnerPublisher>().SpawningEvent += MapGeneratorComplete; }

    /// <summary>
    /// Called once the spawner publisher emits
    /// </summary>
    //private void MapGeneratorComplete() { isMapGeneratorComplete = true; }

    /// <summary>
    /// Spawns an enemy on the biome if the random number is greater than 90.
    /// This provides a 40% chance to spawn an enemy. 
    /// </summary>
    private void OnSpawning()
    {
        float randomNumber = Random.Range(0f, 101f);
        //Enemies
        if (randomNumber > (100 - SpawnManagerABL.SpawnChances) && SpawnManagerABL.totalEnemyCount < SpawnManagerABL.MaxEnemyCount)
        {
            enemyTypesArray = enemyTypes.ToArray();
            GameObject enemy = enemyTypesArray[Random.Range(0, 2)];
            enemy.GetComponent<EnemyController>().path = path;

            if (path.transform.childCount > 0) {
                InstantiateOnTop(enemy);
                SpawnManagerABL.totalEnemyCount++;
            }
        }
        //Items
        if (randomNumber > 40 && SpawnManagerABL.totalItemsCount < SpawnManagerABL.MaxItemCount && !occupiedLocations.Contains(gameObject.transform.position)) {
            InstantiateOnTop(items[Random.Range(0, 3)]);
            SpawnManagerABL.totalItemsCount++;
        }

        //Hazards
        if (randomNumber > 40 && SpawnManagerABL.totalHazardsCount < SpawnManagerABL.MaxHazardCount && !occupiedLocations.Contains(gameObject.transform.position)) {
            InstantiateOnTop(spikes);
            SpawnManagerABL.totalHazardsCount++;
        }
    }

    /// <summary>
    /// Ensures the position of the enemy is on a navmesh surface
    /// </summary>
    /// <param name="toSpawn"></param>
    /// <param name="platform"></param>
    private void InstantiateOnTop(GameObject toSpawn)
    {
        if (toSpawn != null)
        {
            Vector3 biomePosition = transform.position;

            float biomeHeight = transform.localScale.y;
            float enemyHeight = toSpawn.transform.localScale.y;

            Vector3 toSpawnPosition = new(
                biomePosition.x,
                biomePosition.y + (biomeHeight / 2) + (enemyHeight / 2),
                biomePosition.z
            );
            
            if (NavMesh.SamplePosition(toSpawnPosition, out NavMeshHit hit, 1.0f, NavMesh.AllAreas)) {
                Instantiate(toSpawn, hit.position, Quaternion.identity);
                occupiedLocations.Add(biomePosition);
            }            
        }        
    }
}
