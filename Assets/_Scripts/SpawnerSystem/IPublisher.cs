using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taco_Frog.Assets._Scripts.SpawnerSystem
{
    public interface IPublisher
    {
        public void Subscriber(ISubscriber subscriber);
        public void UnSubscriber(ISubscriber subscriber );
        public void Notify(float h, float l);

       
    }

    public interface UIPublisher
    {
         public void Subscriber(IUISubscriber subscriber);
         public void NotifyInvincibilityUI(bool act);
    }
}