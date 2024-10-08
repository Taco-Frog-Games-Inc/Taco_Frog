internal interface ILevelDamage
{
    public float dmgTakenFromEnemy {get;}
    public float dmgTakenFromRangedEnemy {get;}
    public float dmgTakenFromLava {get;}
}

internal interface ILevelDamageToEnemy
{
    public float dmgTakenFromPlayer {get;}
   //public float dmgTakenFromRangedEnemy {get;}
   // public float dmgTakenFromEnv {get;}
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




public class EasyLevelDamageToEnemy : ILevelDamageToEnemy
{
    public float dmgTakenFromPlayer => 30f;
   // public float dmgTakenFromRangedEnemy => 5f;
   // public float dmgTakenFromEnv => 20f;
}

public class MediumLevelDamageToEnemy : ILevelDamageToEnemy
{
    public float dmgTakenFromPlayer => 10f;
    //public float dmgTakenFromRangedEnemy => 1f;
   // public float dmgTakenFromLava => 40f;
}

public class HardLevelDamageToEnemy : ILevelDamageToEnemy
{
    public float dmgTakenFromPlayer => 5f;
  //  public float dmgTakenFromRangedEnemy => 3f;
   // public float dmgTakenFromLava => 80f;
}