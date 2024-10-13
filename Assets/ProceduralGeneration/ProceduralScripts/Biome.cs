using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome : MonoBehaviour
{
    private GameObject spawnPoint;    

    [Header("SpawnObjects")]
    [SerializeField] private List<GameObject> enemyTypes = new();
    [SerializeField] private GameObject spawnerPublisher;

    private GameObject path;

    private GameObject[] enemyTypesArray;

    private void OnEnable()
    {
        Subscribe();
        path = GameObject.Find("Path");
        spawnPoint = gameObject.transform.GetChild(0).gameObject;

        //GameObject waypoint = Instantiate(spawnPoint);
        //waypoint.transform.SetParent(path.transform, false);        

        // Instantiate the waypoint
        GameObject waypoint = Instantiate(spawnPoint);

        // Keep world position, then set as a child
        Vector3 worldPosition = waypoint.transform.position;
        waypoint.transform.SetParent(path.transform, false); // Set parent without affecting position
        waypoint.transform.position = worldPosition; // Restore world position
    }

    public void Subscribe() {
        spawnerPublisher.GetComponent<SpawnerPublisher>().SpawningEvent += OnSpawning;
    }

    private void OnSpawning() {
        float randomNumber = Random.Range(0f, 101f);
        if (randomNumber > 50)
        {
            enemyTypesArray = enemyTypes.ToArray();
            GameObject enemy = enemyTypesArray[Random.Range(0, enemyTypesArray.Length)];
            enemy.gameObject.GetComponent<EnemyController>().path = path;
            if (path.transform.childCount > 0)
                Instantiate(enemy, transform.position, Quaternion.identity);
            else
                Debug.Log("No waypoints...");
        }
    }
}
