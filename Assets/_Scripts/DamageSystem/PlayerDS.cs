using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDS : DamageSystem
{
    protected readonly GameObject _enemy;
    protected readonly GameObject _environment;

    private  float _damage; //this will be changes once health systme is implemented\
    private  float _rangeDamage;
    private float _lavaDamage;

    private string _levelDiff;

    private float damageTakenPerSecondBase = 1000;

    private float timer = 0f;

    public PlayerDS(GameObject entity, GameObject env, string diff) : base(entity)
    {
        _levelDiff = diff;
       
        entity = _enemy;
        env = _environment;
        timer = 0f;

    }

    public override float DamageTaken(float damage)
    {
        //timer +=(float)1/damageTakenPerSecondBase; //this values can be changes as per the speed required to damage the player
        timer += Time.deltaTime;
        if (timer > 3f)
        {
            SetDifficulty(_levelDiff);
            damage -= _damage;
            timer = 0f;
            Debug.Log($"Damage applied: {_damage}");
        }

        return damage;

    }

   
    public void SetDifficulty(string diff)
    {
        switch(diff.ToLower())
        {
            case "easy":
                var diffDmg = new EasyLevelDamage();
                _damage = diffDmg.dmgTakenFromEnemy;
                 Debug.LogError($"Damage on easy {_damage}");
                _rangeDamage = diffDmg.dmgTakenFromRangedEnemy;
                _lavaDamage = diffDmg.dmgTakenFromLava;
                break;
            case "medium":
                var diffDmg2 = new MediumLevelDamage();
                _damage = diffDmg2.dmgTakenFromEnemy;
                _rangeDamage = diffDmg2.dmgTakenFromRangedEnemy;
                _lavaDamage = diffDmg2.dmgTakenFromLava;
                break;
            case "hard":
                var diffDmg3 = new HardLevelDamage();
                _damage = diffDmg3.dmgTakenFromEnemy;
                _rangeDamage = diffDmg3.dmgTakenFromRangedEnemy;
                _lavaDamage = diffDmg3.dmgTakenFromLava;
                break;
                default:
                Debug.LogError("Difficulty not recognized");
                break;
        }
        
    }

   
}
