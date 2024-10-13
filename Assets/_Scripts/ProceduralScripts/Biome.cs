using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        if (gameObject.name.Contains("Grass_1") && !isInitialed && isMapGeneratorComplete && NavMeshManager.isInitialize)
        {
            StartCoroutine(SpawnAgentsAfterBake());
            isInitialed = true;
        }
    }

    private System.Collections.IEnumerator SpawnAgentsAfterBake()
    {        
        yield return null;    
        if(!MapGenerator.hasOneEnemy)
        OnSpawning();
    }

    public void Subscribe()
    {
        spawnerPublisher.GetComponent<SpawnerPublisher>().SpawningEvent += MapGeneratorComplete;
    }

    private void MapGeneratorComplete() { isMapGeneratorComplete = true; }

    private void OnSpawning()
    {
        float randomNumber = Random.Range(0f, 101f);
        if (randomNumber > 90)
        {            
            enemyTypesArray = enemyTypes.ToArray();
            GameObject enemy = enemyTypesArray[0];
            enemy.GetComponent<EnemyController>().path = path;
            
            if (path.transform.childCount > 0)                
                InstantiateCubeOnTop(enemy, gameObject);                                                                                                         
        }
    }

    private void InstantiateCubeOnTop(GameObject enemy, GameObject platform)
    {
        if (platform != null && enemy != null)
        {
            Vector3 targetPosition = platform.transform.position;

            float targetHeight = platform.transform.localScale.y;

            float newCubeHeight = enemy.transform.localScale.y;

            Vector3 newCubePosition = new(
                targetPosition.x,
                targetPosition.y + (targetHeight / 2) + (newCubeHeight / 2),
                targetPosition.z
            );

            NavMeshHit hit;
            if (NavMesh.SamplePosition(newCubePosition, out hit, 1.0f, NavMesh.AllAreas)) {
                Instantiate(enemy, hit.position, Quaternion.identity);
                //MapGenerator.hasOneEnemy = true;
            }        
                
        }        
    }
}
