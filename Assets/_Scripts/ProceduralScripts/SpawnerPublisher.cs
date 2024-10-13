using UnityEngine;


public class SpawnerPublisher : MonoBehaviour
{
    public delegate void SpawnerEventHandler();
    public event SpawnerEventHandler SpawningEvent;
    
    public void PublishMapSignal() { SpawningEvent?.Invoke(); }    
}
