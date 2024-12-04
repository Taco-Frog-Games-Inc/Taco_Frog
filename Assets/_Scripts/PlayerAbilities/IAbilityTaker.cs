
using System.Collections;
using UnityEngine;

public interface IAbilityTaker
{
    
    float GetJumpHeight();
   void SetHeight(float j);
   
}
interface IInvincibility
{
    int GetPlayerHealth();
    void SetPlayerHealth(int d);
}

public abstract class Ability : MonoBehaviour, IAbilityTaker
{
    protected IAbilityTaker player;
    
    public Ability (IAbilityTaker player)
    {
        this.player = player;
    }

    public virtual float GetJumpHeight()
    {
        return player.GetJumpHeight();
    }
     public virtual void SetHeight(float jump)
    {
        jump = player.GetJumpHeight();
    }

   
  

    
}


public class JumpAbility :  Ability
{
    private float jumpBoost, origHeight;
    private bool _isActivated;
    
    public JumpAbility(IAbilityTaker basePlayer, float boost) : base(basePlayer)
    {
        jumpBoost = boost;
        _isActivated = false;
    }
     public JumpAbility(IAbilityTaker basePlayer) : base(basePlayer)
    {
        _isActivated = false;
    }

    

   
    public override float GetJumpHeight()
    {
        if (!_isActivated)
        {
            // Enhance speed once and mark as activated
            origHeight = player.GetJumpHeight();
            float newSpeed = player.GetJumpHeight() + jumpBoost;
           player.SetHeight(newSpeed);
            
            Debug.Log($"{newSpeed}");
            _isActivated = true;
            Debug.Log($"{_isActivated}"); // Ensure the boost only applies once
           
            return newSpeed;
        }

        // Return regular speed after the first activation
        return player.GetJumpHeight();
    }


   
    public bool IsActivated() => _isActivated;




}




