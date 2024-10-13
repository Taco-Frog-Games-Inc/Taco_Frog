using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

/*
 * Source File Name: MapGenerator.cs
 * Author Name: Alexander Maynard
 * Student Number: 301170707
 * Creation Date: October 4th, 2024
 * 
 * Last Modified by: Alexander Maynard
 * Last Modified Date: October 12th, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script handles the generation of the map using perlin noise.
 * 
 * 
 * Revision History:
 *      -> October 4th, 2024:
 *          -Created this script and fully implemented it.
 *      ->October 5th, 2024:
 *          -Wrote comments and refactored some code.
 *          -Added code to add all biomes that are Instantiate as children of the _mapParent (map generator).
 *      ->October 12th, 2024:
 *          -Added logic to decide biome subtypes based on a % chance of happening.
 *          -Added a check to make sure that the tile the player spawns on is always the first grass tile.
 *          -Commented code
 */

public class MapGenerator : MonoBehaviour
{
    //variables to for the map
    #region Map Related Fields
    [Header("Map related fields")]
    [SerializeField] private int _height = 10;
    [SerializeField] private int _length = 10;
    [SerializeField] private float _perlinNoiseScale = 4.0f; //4 seems to be the most natural for our use case
    [SerializeField] private GameObject _mapParent; //gameobject that will parent the biomes for the map.
    private float _offsetX, _offsetZ; //for seed randomization
    private GameObject[,] _levelMap;
    [SerializeField] private GameObject _navMeshSurfaceTile;
    #endregion

    //biome prefabs
    #region Biome Prefab Lists
    [Header("Biome type lists")]
    [SerializeField] private List<GameObject> _waterBiomeTypes;
    [SerializeField] private List<GameObject> _grassBiomeTypes;
    [SerializeField] private List<GameObject> _rockBiomeTypes;
    [SerializeField] private List<GameObject> _lavaBiomeTypes;
    #endregion

    //Biome properties
    [Header("Biome properties")]
    [SerializeField] private GameObject path;
    [SerializeField] private GameObject spawnerPublisher;
    
    /// <summary>
    /// Start created the level double array of gameobjects for the map as 
    /// well as defines the offests for the map to be more randomized each time it is played.
    /// </summary>
    private void Start()
    {
        //set the initial size of the map
        _levelMap = new GameObject[_height, _length];
        //offsets for 'seed' to be randomized
        _offsetX = Random.Range(10000, 50000);
        _offsetZ = Random.Range(10000, -50000); 
        GenerateMap(); //call the generation of the map
        spawnerPublisher.GetComponent<SpawnerPublisher>().Publish();
    }

    /// <summary>
    /// Iterates through each tile as assigns/ places the proper biome tiles
    /// </summary>
    private void GenerateMap()
    {
        //loop for x anz to loop through all cells
        for (int x = 0; x < _height; x++)
        {
            for (int z = 0; z < _length; z++)
            {
                float perlinNoiseValue = CreatePerlinNoiseValue(x, z); //generate a perlin noise value, returns a float from 0-1
                var biomePrefab = GenerateBiome(x, z, perlinNoiseValue); //generate a biome by passign the row/column and the noise value
                //Instantiate the biome prefab (Item.1) at a particular x, z coordiante with the position (Item2) returned.
                _levelMap[x, z] = Instantiate(biomePrefab.Item1, new Vector3(x * 10, biomePrefab.Item2, z * 10), Quaternion.identity, _mapParent.transform); 
            }
        }
    }

    /// <summary>
    /// Generates the perlin noise value
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="z">Z coordinate</param>
    /// <returns></returns>
    private float CreatePerlinNoiseValue(float x, float z)
    {
        //perlin noise formula with offsetX and Z so that it is
        //more randomized each time and more controllable in it's output and with
        //randomized offsets with the scale adjusted as well
        float xCoordinate = (x + _offsetX) / _height * _perlinNoiseScale;
        float zCoordinate = (z + _offsetZ) / _length * _perlinNoiseScale;
        return Mathf.Clamp01(Mathf.PerlinNoise(xCoordinate, zCoordinate));  //return perlin noise value (clamped so no random values)
    }

    /// <summary>
    /// Generates each biome for use by the map
    /// </summary>
    /// <param name="xCoordinate">x coordinate from the current map cell</param>
    /// <param name="zCoordinate">z coordinate from the current map cell</param>
    /// <param name="perlinNoisevalue">Perlin noise value to get more natural procedural generation values being chosen at 'random'</param>
    /// <returns></returns>
    private (GameObject, float) GenerateBiome(int xCoordinate, int zCoordinate, float perlinNoisevalue)
    {
        GameObject biomePrefab = null; //set the current biomePrefab as null to instantiate it
        float yTransformOfPrefab = 0; //set the current biomePrefab yTransform to 0 to instantiate it
        int biomeSubTypeChoice = 0; //set the current


        //check the player spawn point to make sure that the first tile is always the first grass one
        if(xCoordinate == 0 && zCoordinate == 0)
        {
            biomePrefab = _navMeshSurfaceTile;
            return (biomePrefab, biomePrefab.GetComponent<Renderer>().bounds.size.y / 2);
        }



        //check the perlin value for the material from the list
        switch (perlinNoisevalue)
        {
            //water biome value range
            case <= 0.2f:
                //pick water biome  subtype value 
                biomeSubTypeChoice = Random.Range(0, 101);

                //pick random water biome subtypoe based on %
                switch (biomeSubTypeChoice)
                {
                    case <= 80:
                        biomePrefab = _waterBiomeTypes[0]; //80% chance of this tile
                        break;
                    case <= 100:
                        biomePrefab = _waterBiomeTypes[1]; //20% chance of this tile
                        break;
                    default:
                        biomePrefab = _waterBiomeTypes[0]; //default
                        break;
                }
                break;
            case <= 0.60f:
                //pick grass biome subtype value 
                biomeSubTypeChoice = Random.Range(0, 101);

                //pick random grass biome subtypoe based on %
                switch (biomeSubTypeChoice)
                {
                    case <= 45:
                        biomePrefab = _grassBiomeTypes[0]; //45% chance of this tile
                        break;
                    case <= 60:
                        biomePrefab = _grassBiomeTypes[1]; //15% chance of this tile
                        break;
                    case <= 75:
                        biomePrefab = _grassBiomeTypes[2]; //15% chance of this tile
                        break;
                    case <= 90:
                        biomePrefab = _grassBiomeTypes[3]; //15% chance of this tile
                        break;
                    case <= 95:
                        biomePrefab = _grassBiomeTypes[4]; //5% chance of this tile
                        break;
                    case <= 100:
                        biomePrefab = _grassBiomeTypes[5]; //5% chance of this tile
                        break;
                    default:
                        biomePrefab = _grassBiomeTypes[0]; //default
                        break;
                }
                break;
            //rock biome value range
            case <= 0.85f:
                //catch so that mountains don't go to the very edge of the map and potentially block items or level end, etc.
                if (xCoordinate == 0 || xCoordinate == _height - 1 || zCoordinate == 0 || zCoordinate == _length - 1)
                {
                    biomePrefab = _grassBiomeTypes[Random.Range(0, _grassBiomeTypes.Count)]; //picks random grass subtype to replace teh mountain piece
                }
                else
                {
                    biomePrefab = _rockBiomeTypes[Random.Range(0, _rockBiomeTypes.Count)]; //picks random subtype, all have an equal chance of spawning
                }
                break;
            //lava biome value range
            case <= 1.0f:
                //pick water biome  subtype value 
                biomeSubTypeChoice = Random.Range(0, 101);

                //pick random lava biome subtypoe based on %
                switch (biomeSubTypeChoice)
                {
                    case <= 40:
                        biomePrefab = _lavaBiomeTypes[0]; //40% chance of this tile
                        break;
                    case <= 80:
                        biomePrefab = _lavaBiomeTypes[1]; //40% chance of this tile
                        break;
                    case <= 100:
                        biomePrefab = _lavaBiomeTypes[2]; //20% chance of this tile
                        break;
                    default:
                        biomePrefab = _lavaBiomeTypes[0]; //default
                        break;
                }
                break;
        }

        //adjust the y transform of the values to get a cool 'blocky' look where some biomes are taller than others.
        //"R" & "G" (rock and grass) must be touching the 0 y transform from underneath and the others are the opposite.
        if (biomePrefab.name.Substring(0, 1) == "R" || biomePrefab.name.Substring(0, 1) == "G") 
            yTransformOfPrefab = biomePrefab.GetComponent<Renderer>().bounds.size.y / 2;//find the y value by calcualting half the object height. Otherwise all items spawn at their center.
        else
            yTransformOfPrefab = -biomePrefab.GetComponent<Renderer>().bounds.size.y / 2;//find the y value by calcualting half the object height. Then reverse it. Otherwise all items spawn at their center.

        return (biomePrefab, yTransformOfPrefab); //return the biome object and it's transform
    }
}