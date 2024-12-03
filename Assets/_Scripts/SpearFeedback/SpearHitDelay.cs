using UnityEngine;

/*
 * Source File Name: PlayerKnockback.cs
 * Author Name: Aledxander Maynard
 * Student Number: 301170707
 * Creation Date:  December 2nd, 2024
 * 
 * Last Modified by: Alexander Maynard
 * Last Modified Date: December 2nd, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script handles hit delay between spear hits.
 * 
 * Revision History:
 *      -> December 32nd, 2024:
 *          -Created the and implemented this script.
 */

public class SpearHitDelay : MonoBehaviour
{
    [SerializeField] private float _spearDelayTime = 1.5f;
    [SerializeField] public float _currentSpearDelayTime = 0;
    [SerializeField] private Collider _spearCollider; //for damage detection
    [SerializeField] private Collider _spearSplashCollider; //for splash detection
    private bool _startCountdown = false;


    /// <summary>
    /// countdown for the spear collider.
    /// </summary>
    private void Update()
    {
        if(_startCountdown)
        {
            _currentSpearDelayTime -= Time.deltaTime;
            if(_currentSpearDelayTime <= 0 )
            {
                _spearCollider.enabled = true;
                _spearSplashCollider.enabled = true;
                _startCountdown = false;
            }
        }
    }

    /// <summary>
    /// Detects if the spear hits the player. If so disable the spear and start the countdown.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _spearCollider.enabled = false;
            _spearSplashCollider.enabled = false;
            _startCountdown = true;
            _currentSpearDelayTime = _spearDelayTime;
        }
    }
}
