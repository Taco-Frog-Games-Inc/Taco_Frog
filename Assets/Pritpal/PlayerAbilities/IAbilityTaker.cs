
using System.Collections;
using UnityEngine;

public interface IAbilityTaker
{
    
    float GetJumpHeight();
   void SetHeight(float j);
   // bool IsJumpPressed();
   //void Run();
}
interface IInvincibility
{
    int GetDamage();
    void SetDamage(int d);
}

public abstract class Ability : MonoBehaviour, IAbilityTaker, IInvincibility
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

    public virtual int GetDamage()
    {
        return 0;
    }
    public virtual void SetDamage(int d)
    {

    }

  

    
}


class JumpAbility :  Ability
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



class InvincibleAbility : IInvincibility
{
    private bool _isActivated;
    private IInvincibility spear, projectile;
    

    public InvincibleAbility(IInvincibility spear)
    {
        this.spear = spear;
       // this.projectile = projectile;
    }
    public int GetDamage()
    {
        if(!_isActivated)
        {
           int newDamage = spear.GetDamage() * 0;
            SetDamage(newDamage);
           _isActivated = true;
           return newDamage;
        }

        return spear.GetDamage();
    }
    public void SetDamage(int a)
    {
        
        Debug.Log($"{spear.GetDamage()}");
        projectile.SetDamage(a);
        Debug.Log($"{projectile.GetDamage()}");
        spear.SetDamage(a);
    }
    

}