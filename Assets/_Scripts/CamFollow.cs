using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] private Transform _playerTransfom;

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = _playerTransfom.position;
    }
}
