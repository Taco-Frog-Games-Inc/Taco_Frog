using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome : MonoBehaviour
{
    private GameObject spawnPoint;    

    [Header("SpawnObjects")]
    [SerializeField] private List<GameObject> enemyTypes = new();
    private GameObject path;

    private GameObject[] enemyTypesArray;
    
    // Start is called before the first frame update
    void Start()
    {
        path = GameObject.Find("Path");
        spawnPoint = gameObject.transform.GetChild(0).gameObject; 
        //Instantiate the point as a child of the point gameObject.
        GameObject waypoint = Instantiate(spawnPoint);
        waypoint.transform.SetParent(path.transform, false);

        
        float randomNumber = Random.Range(0f, 101f);
        if (randomNumber > 50) {
            enemyTypesArray = enemyTypes.ToArray();
            Instantiate(enemyTypesArray[Random.Range(0, enemyTypesArray.Length)], transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
