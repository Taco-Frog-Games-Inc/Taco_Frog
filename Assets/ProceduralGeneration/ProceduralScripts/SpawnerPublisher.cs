using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class SpawnerPublisher : MonoBehaviour
{
    public delegate void SpawnerEventHandler();
    public event SpawnerEventHandler SpawningEvent;
   
    public void Publish() {
        SpawningEvent?.Invoke();
    }
}
