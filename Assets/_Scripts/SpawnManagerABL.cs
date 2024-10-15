using UnityEngine;

/*
 * Source File Name: SpawnManagerABL.cs
 * Author Name: Audrey Bernier Larose
 * Student Number: 301166198
 * Creation Date: October 14th, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: October 14th, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script handling the amount of items to spawn in the world.
 * 
 * Revision History:
 *      -> October 14th, 2024:
 *          -Created this script and fully implemented it. 
 */
public class SpawnManagerABL: MonoBehaviour 
{
    [Header("Enemy")]
    [SerializeField] private int maxEnemyCount;

    [Header("Items")]
    [SerializeField] private int maxItemCount;

    [Header("Hazards")]
    [SerializeField] private int maxHazardCount;

    public static int totalEnemyCount;
    public static int totalItemsCount;
    public static int totalHazardsCount;


    //Readonly
    public int MaxItemCount { get { return maxItemCount; } }
    public int MaxEnemyCount { get { return maxEnemyCount; } }
    public int MaxHazardCount { get { return maxEnemyCount; } }
}
