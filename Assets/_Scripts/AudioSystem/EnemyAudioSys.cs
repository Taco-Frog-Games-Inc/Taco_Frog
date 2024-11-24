using System.Collections.Generic;


public static class EnemyAudioSystem 
{

    
    private static List<string> audioNames = new List<string>{"e_walking",  "e_attack", "e_getDamage",  "e_died"};

    public static List<string> GetEnemyAudioNames()
    {
        return audioNames;
    }
   
}