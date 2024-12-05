using UnityEngine;
using System.Collections.Generic;

/*
 * Source File Name: SpawnManagerABL.cs
 * Author Name: Audrey Bernier Larose
 * Student Number: 301166198
 * Creation Date: October 14th, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: December 5th, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script handling the amount of items to spawn in the world.
 * 
 * Revision History:
 *      -> October 14th, 2024:
 *          -Created this script and fully implemented it. 
 *      -> December 5th, 2024:
 *          -Adjusted for difficulty
 */
public class SpawnManagerABL: MonoBehaviour {

    public static int totalEnemyCount;
    public static int totalItemsCount;
    public static int totalHazardsCount;

    public static int MaxItemCount { get; set; } = 0;
    public static int MaxEnemyCount { get; set; } = 0;
    public static int MaxHazardCount { get; set; } = 0;
    public static int EnemySpeed { get; set; } = 0;
    public static int SpawnChances { get; set; } = 0;

    private void Start() {
        int multipler;
        switch (SceneManagement.difficultyLevel) {
            case Difficulty.Easy:
                multipler = Globals.DataDiffMultiplierDict[Difficulty.Easy];
                break;

            case Difficulty.Medium:
                multipler = Globals.DataDiffMultiplierDict[Difficulty.Medium];
                break;

            case Difficulty.Hard:
                multipler = Globals.DataDiffMultiplierDict[Difficulty.Hard];
                break;

            default:
                multipler = Globals.DataDiffMultiplierDict[Difficulty.Easy];
                break;
        }
        SetDifficultyData(multipler);
    }

    private void SetDifficultyData(int multiplier) {
        foreach (var item in Globals.DataDiffValueDict) {
            var property = typeof(SpawnManagerABL).GetProperty(item.Key,
                                                    System.Reflection.BindingFlags.Static | 
                                                    System.Reflection.BindingFlags.Public | 
                                                    System.Reflection.BindingFlags.NonPublic);

            if (property == null) return;
            if (!property.CanWrite) return;

            property.SetValue(this, item.Value * multiplier);                    
        }
    }
}

public static class Globals {
    
    private static Dictionary<Difficulty, int> dataDiffMultiplierDict = new() {
        { Difficulty.Easy, 1 },
        { Difficulty.Medium, 2 },
        { Difficulty.Hard, 3 }
    };

    private static Dictionary<string, int> dataDiffValueDict = new() {
        { nameof(SpawnManagerABL.MaxItemCount), 6 },
        { nameof(SpawnManagerABL.MaxEnemyCount), 2 },
        { nameof(SpawnManagerABL.MaxHazardCount), 2 },
        { nameof(SpawnManagerABL.SpawnChances), 30 },
        { nameof(SpawnManagerABL.EnemySpeed), 1 }
    };

    public static Dictionary<Difficulty, int> DataDiffMultiplierDict { get { return dataDiffMultiplierDict; } }
    public static Dictionary<string, int> DataDiffValueDict { get { return dataDiffValueDict; } }
}