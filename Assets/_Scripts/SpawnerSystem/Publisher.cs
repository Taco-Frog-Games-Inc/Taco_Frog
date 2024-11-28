using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taco_Frog.Assets._Scripts.SpawnerSystem
{
    public abstract class Publisher : IPublisher
{
    private List<ISubscriber> subscribers = new();
   

     public void Subscriber(ISubscriber subscriber)
    {
        if (!subscribers.Contains(subscriber))
            subscribers.Add(subscriber);
    }

    public void UnSubscriber(ISubscriber subscriber)
    {
        if (subscribers.Contains(subscriber))
            subscribers.Remove(subscriber);
    }
   

    public void Notify(float h, float l)
    {
        foreach (ISubscriber subscriber in subscribers)
        {
            subscriber.Update_hl(h,l);
        }
    }

   
}
}