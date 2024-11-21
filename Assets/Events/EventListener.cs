using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EventListener<T> : MonoBehaviour
{
     [SerializeField] private EventChannel<T> _channel;
    [SerializeField] private UnityEvent<T> _unityEvent;

    protected void Awake()
    {
        _channel.Register(this); //adds to the listeners list
    }

    protected void OnDestroy()
    {
        _channel.Deregister(this);
    }

    public void Raise(T value)
    {
        _unityEvent?.Invoke(value);
    }
}
