using System.Collections.Generic;


public static class PlayerAudioSystem 
{

    
    private static List<string> audioNames = new List<string>{"p_walking", "p_jumping", "p_attack", "powerUp", "p_getDamage","p_jumpEnhance", "p_drown", "_pickup"};

    public static List<string> GetPlayerAudioNames()
    {
        return audioNames;
    }
   
}