using UnityEngine;

/*
 * Source File Name: SplashParticleManager.cs
 * Author Name: Aledxander Maynard
 * Student Number: 301170707
 * Creation Date:  November 30th, 2024
 * 
 * Last Modified by: Alexander Maynard
 * Last Modified Date: December 2nd, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script handles the particles to be played for the player and enemies.
 * 
 * Revision History:
 *      -> November 30th, 2024:
 *          -Created the initial calling of particles for the player (no colors yet).
 *      -> December 2nd, 2024:
 *          -Updated the script to change particle colors based on what the player hits.
 */


public class SplashParticleManager : MonoBehaviour
{
    [SerializeField] private Color _lavaColor;
    [SerializeField] private Color _waterColor;
    [SerializeField] private Color _bloodColor;
    [SerializeField] private Color _enemyBloodColor;
    [SerializeField] private ParticleSystem _splashParticleSystem;
    [SerializeField] private EnemyHealth _enemyhealth;

    /// <summary>
    /// On a collision decide what hit the player. Then passs a color the the ChangeColor method.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            ChangeColor(_waterColor);
        }
        else if (other.gameObject.CompareTag("Lava"))
        {
            ChangeColor(_lavaColor);
        }
        else if (other.gameObject.CompareTag("SpearHead") || other.gameObject.CompareTag("Projectile"))
        {
            ChangeColor(_bloodColor);
        }
        //check so that player components don't cause particles to play on start.
        else if (!this.gameObject.CompareTag("PlayerParticles") && (other.gameObject.CompareTag("Tongue") || other.gameObject.CompareTag("EnemySquasher")))
        {
            ChangeColor(_enemyBloodColor);
        }
    }

    /// <summary>
    /// Changes the color of the particles with the color passed in 
    /// </summary>
    /// <param name="color">color that is going to be applied the partcile effect</param>
    private void ChangeColor(Color color)
    {
        var main = _splashParticleSystem.main;
        main.startColor = new ParticleSystem.MinMaxGradient(color);
        _splashParticleSystem.Clear();
        _splashParticleSystem.Play();
    }
}
