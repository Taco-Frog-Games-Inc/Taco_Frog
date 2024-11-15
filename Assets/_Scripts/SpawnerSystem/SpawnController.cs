using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour, ISubscriber
{
    [Header(" Spawn Station Position")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Vector3 mapSize;
    //[SerializeField] private GameObject map;


    [Header("Item Spawn Station")]
    [SerializeField] private GameObject saucePrefab;
    [SerializeField] private GameObject jumpPowerPickup;
    [SerializeField] private GameObject invPowerPickup;




    SpawnManager hotSuacePickup, jumpPowerUpPickup, invincibilityPickup;

    float height;
    float width;
    bool subscribed = false;

    void Start()
    {

        mapSize.x = height;
        mapSize.z = width;
        Debug.Log($"mapSize set to: {mapSize}");

        hotSuacePickup = new HotSuacePickup(saucePrefab, groundMask, mapSize, true);
        jumpPowerUpPickup = new PowerPickUp(jumpPowerPickup,groundMask,mapSize,true);
        invincibilityPickup = new PowerPickUp(invPowerPickup, groundMask, mapSize, true);

       // Debug.Log("EnemySpawner and HotSuacePickup instances created.");


    }


   
    public void Update_hl(float h, float l)
    {
        height = h;
        width = l;
        subscribed = true;
    }
}
