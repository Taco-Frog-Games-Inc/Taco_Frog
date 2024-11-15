using System.Collections;
using System.Collections.Generic;
using Taco_Frog.Assets._Scripts.SpawnerSystem;
using UnityEngine;

public class Invincibility : MonoBehaviour, UIPublisher
{

   
    const string playerTag = "Player";
    private List<IUISubscriber> uiSubscribers = new();
   
    bool _isTriggered = true;

    private IUISubscriber subscriber;
    void Start()
    {
        subscriber = GameObject.FindWithTag("PowerUp").GetComponent<InvincibilityUI>();
        Subscriber(subscriber);
    }

    void Update()
    {
        var rotation = new Quaternion(this.transform.rotation.x,this.transform.rotation.y,this.transform.rotation.z * Time.deltaTime, 1f );
    }
    void OnTriggerEnter(Collider other)
    {
        var playerScr = other.GetComponent<TestPlayerController>();
        if(other.CompareTag(playerTag))
        {
            playerScr.health = 5;
            Debug.Log("Trigger activated!");
            NotifyInvincibilityUI(_isTriggered);
            Destroy(this.gameObject, 1f);
        }
    }

     public void Subscriber(IUISubscriber subscriber)
     {
        if(!uiSubscribers.Contains(subscriber))
            uiSubscribers.Add(subscriber);
     }
    public void NotifyInvincibilityUI(bool activate)
    {
        Debug.Log("NotifyInvincibilityUI Called!");
        foreach(var sub in uiSubscribers)
        {
              Debug.Log("NotifyInvincibilityUI Called!");
              sub.UpdateUI(activate, sub);
        }
            
    }
}
