using System.Security.Cryptography;
using UnityEngine;

/*
 * Source File Name: Items.cs
 * Author Name: Audrey Bernier Larose
 * Student Number: 301166198
 * Creation Date: October 14th, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: November 10th, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script provides items with a way to give points to an object that implements the IRewardTaker
 * 
 * Revision History:
 *      -> October 14th, 2024:
 *          -Created this script and fully implemented it.
 *      -> November 10th, 2024:
 *          -Implemented the Start(), Update() and ItemTypeEnum
 */
public class Items : MonoBehaviour, IRewarder
{
    [Header("Properties")]
    [SerializeField] private int itemValue;
    [SerializeField] private ItemTypeEnum itemType;
    public int RewardToGive { get { return itemValue; } set { if (value > 0) itemValue = value; } }

    /// <summary>
    /// Parts of the IRewarder contract.
    /// Checks if the other collider is implementing the IRewardTaker interface.
    /// It if does, calls the IncreaseScore() of the IRewardTaker interface.
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<IRewardTaker>(out var rewardTaker)) {
            rewardTaker.IncreaseScore(RewardToGive, itemType);
            Destroy(gameObject);
        }                
    }

    void Start() {
        Vector3 newPos = new(transform.position.x, 1.5f, transform.position.z);
        transform.position = newPos;
    }

    void Update() {
        transform.Rotate(0, 0,5f, 0);
    }
}

public enum ItemTypeEnum { 
    Diamond,
    Coin
}
