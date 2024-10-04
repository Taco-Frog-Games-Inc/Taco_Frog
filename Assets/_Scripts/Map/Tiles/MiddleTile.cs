using System.Collections.Generic;
using UnityEngine;

public class MiddleTile : Tile
{
    //Add properties specific to middle tile...

    [SerializeField] private List<GameObject> biomes;

    void Start()
    {
        int chances = Random.Range(0, 4);
        int index = Random.Range(0, biomes.Count - 1);
        GameObject biome = biomes.ToArray()[index];
        if (chances > 2) {
            Vector3 pos = new(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            Instantiate(biome, pos, Quaternion.identity);
        }            
    }    
}
