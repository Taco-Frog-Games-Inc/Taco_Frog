using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Source File Name: CamFollow.cs
 * Author Name: Alexander Maynard
 * Student Number: 301170707
 * Creation Date: October 2nd, 2024
 * 
 * Last Modified by: Alexander Maynard
 * Last Modified Date: October 2nd, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script handles the Camera Follow point gameboject's position, which follows the player position at all times. 
 *      This allows the Cinemachine camera to follow the player without rotating when the player does.
 * 
 * 
 * Revision History:
 *      -> October 2nd, 2024:
 *          -Created this script and fully implemented it.
 */

public class CamFollow : MonoBehaviour
{
    //transform of the player.
    [SerializeField] private Transform _playerTransfom;

    /// <summary>
    /// Follow the player's exact position.
    /// </summary>
    private void FixedUpdate()
    {
        this.transform.position = _playerTransfom.position;
    }
}
