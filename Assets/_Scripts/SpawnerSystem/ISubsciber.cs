using Unity.VisualScripting.YamlDotNet.Core.Tokens;

public interface ISubscriber
{
    public void Update_hl(float height, float _length);
    
}


public interface IUISubscriber
{
    public void UpdateUI(bool act, IUISubscriber sub);
}