using UnityEngine;
/*
 * Source File Name: CamFollow.cs
 * Author Name: Alexander Maynard
 * Student Number: 301170707
 * Creation Date: October 2nd, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: October 14th, 2024
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
 *      -> October 11th, 2024:
 *          -Updated camfollow so that it only follows on x and z axis
 *      -> Modified the script so a regular camera follows the player with a third-person view
 */

public class CamFollow : MonoBehaviour
{
    //transform of the player.
    [SerializeField] private Transform _playerTransfom;

    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    void Start()
    {
        offset = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void LateUpdate()
    {
        if (_playerTransfom != null)
        {
            Vector3 desiredPosition = _playerTransfom.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    /// <summary>
    /// Follow the player's exact position.
    /// </summary>
    /*private void FixedUpdate()
    {
        transform.position = new Vector3(_playerTransfom.position.x, transform.position.y, _playerTransfom.position.z);
    }
    */
}