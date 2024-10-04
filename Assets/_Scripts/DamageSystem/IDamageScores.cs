internal interface ILevelDamage
{
    public float dmgTakenFromEnemy {get;}
    public float dmgTakenFromRangedEnemy {get;}
    public float dmgTakenFromLava {get;}
}




public class EasyLevelDamage : ILevelDamage
{
    public float dmgTakenFromEnemy => 10f;
    public float dmgTakenFromRangedEnemy => 5f;
    public float dmgTakenFromLava => 20f;
}

public class MediumLevelDamage : ILevelDamage
{
    public float dmgTakenFromEnemy => 20f;
    public float dmgTakenFromRangedEnemy => 1f;
    public float dmgTakenFromLava => 40f;
}

public class HardLevelDamage : ILevelDamage
{
    public float dmgTakenFromEnemy => 40f;
    public float dmgTakenFromRangedEnemy => 3f;
    public float dmgTakenFromLava => 80f;
}