using PlasticPipe.PlasticProtocol.Messages;
using Taco_Frog.Assets._Scripts.SpawnerSystem;

public class CheckMapGen : Publisher
{
    //private float height, lenght;

    public void SetHeightLength(float h, float l)
    {
        Notify(h,l);
    }
}