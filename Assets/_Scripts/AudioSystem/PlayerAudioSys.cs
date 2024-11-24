using System.Collections.Generic;


public static class PlayerAudioSystem 
{

    
    private static List<string> audioNames = new List<string>{"p_walking", "p_jumping", "p_attack", "powerUp", "p_getHealth", "p_died"};

    public static List<string> GetPlayerAudioNames()
    {
        return audioNames;
    }
   
}