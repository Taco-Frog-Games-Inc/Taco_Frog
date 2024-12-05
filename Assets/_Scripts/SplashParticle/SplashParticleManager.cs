using UnityEngine;

/*
 * Source File Name: SplashParticleManager.cs
 * Author Name: Aledxander Maynard
 * Student Number: 301170707
 * Creation Date:  November 30th, 2024
 * 
 * Last Modified by: Alexander Maynard
 * Last Modified Date: December 4th, 2024
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
 *      -> December 4th, 2024:
 *          -Fixed broken particle effects.
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
    /// 

    void Start()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            PlayParticlesWithColor(_waterColor);
        }
        else if (other.gameObject.CompareTag("Lava"))
        {
            PlayParticlesWithColor(_lavaColor);
        }
        //check so that enemy components don't cause particles to play on start.
        else if ((!this.gameObject.CompareTag("EnemyParticles") && other.gameObject.CompareTag("SpearHead")) || (!this.gameObject.CompareTag("EnemyParticles") && other.gameObject.CompareTag("Projectile")))
        {
            PlayParticlesWithColor(_bloodColor);
        }
        //check so that player components don't cause particles to play on start.
        else if ((!this.gameObject.CompareTag("PlayerParticles") && (other.gameObject.CompareTag("Tongue")) || (!this.gameObject.CompareTag("PlayerParticles") && other.gameObject.CompareTag("EnemySquasher"))))
        {
            PlayParticlesWithColor(_enemyBloodColor);
        }
    }

    /// <summary>
    /// Changes the color of the particles with the color passed in 
    /// </summary>
    /// <param name="color">color that is going to be applied the partcile effect</param>
    private void PlayParticlesWithColor(Color color)
    {
        var main = _splashParticleSystem.main;
        main.startColor = new ParticleSystem.MinMaxGradient(color);
        _splashParticleSystem.Clear();
        _splashParticleSystem.Play();
    }
}
