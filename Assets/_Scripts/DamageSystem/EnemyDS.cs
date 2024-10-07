using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class EnemyDS : DamageSystem
{
    protected readonly GameObject _player;
    protected readonly GameObject _environment;

    private float _damage; //this will be changes once health systme is implemented\
    private float _rangeDamage;
    private float _envDamage;

    private string _levelDiff;


    public EnemyDS(GameObject entity, GameObject env, string diff) : base(entity)
    {
        _levelDiff = diff;
        _player = entity;
        _environment = env;
        SetDifficulty(_levelDiff);

    }

    public override float DamageTaken(float damage)
    {


        damage -= _damage;

        Debug.Log($"Damage applied: {_damage}");


        return damage;

    }


    public void SetDifficulty(string diff)
    {
        switch (diff.ToLower())
        {
            case "easy":
                var diffDmg = new EasyLevelDamageToEnemy();  //change these classes as well
                _damage = diffDmg.dmgTakenFromPlayer;
                Debug.LogError($"Damage on easy {_damage}");
                //  _rangeDamage = diffDmg.dmgTakenFromRangedEnemy;
                //  _envDamage = diffDmg.dmgTakenFromLava;
                break;
            case "medium":
                var diffDmg2 = new MediumLevelDamageToEnemy();
                _damage = diffDmg2.dmgTakenFromPlayer;
                //    _rangeDamage = diffDmg2.dmgTakenFromRangedEnemy;
                //   _envDamage = diffDmg2.dmgTakenFromLava;
                break;
            case "hard":
                var diffDmg3 = new HardLevelDamageToEnemy();
                _damage = diffDmg3.dmgTakenFromPlayer;
                //   _rangeDamage = diffDmg3.dmgTakenFromRangedEnemy;
                //   _envDamage = diffDmg3.dmgTakenFromLava;
                break;
            default:
                Debug.LogError("Difficulty not recognized");
                break;
        }

    }
}
