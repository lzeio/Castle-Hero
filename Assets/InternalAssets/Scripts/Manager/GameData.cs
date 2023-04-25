using UnityEngine;

public class GameData 
{
    public static void SetCurrentQuality(int index) { PlayerPrefs.SetInt("CurrentQuality_",index); }
    public static int GetCurrentQuality() { return PlayerPrefs.GetInt("CurrentQuality_", 2); }

    
}
