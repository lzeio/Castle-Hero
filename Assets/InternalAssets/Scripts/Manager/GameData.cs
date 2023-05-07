using UnityEngine;

public class GameData 
{
    public static void SetCurrentQuality(int index) { PlayerPrefs.SetInt("CurrentQuality_",index); }

    public static int GetCurrentQuality() { return PlayerPrefs.GetInt("CurrentQuality_", 2); }

    public static void SetMusicLevel(float index) { PlayerPrefs.SetFloat("MusicLevel_", index); }

    public static float GetMusicLevel() { return PlayerPrefs.GetFloat("MusicLevel_", 1); }

    public static void SetSoundLevel(float index) { PlayerPrefs.SetFloat("SoundLevel_", index); }

    public static float GetSoundLevel() { return PlayerPrefs.GetFloat("SoundLevel_", 1); }

    public static void SetEasyWave(int index) { PlayerPrefs.SetInt("Easy_Wave",index);}

    public static int GetEasyWave() { return PlayerPrefs.GetInt("Easy_Wave", 0); }
    
    public static void SetMediumWave(int index) { PlayerPrefs.SetInt("Medium_Wave",index);}

    public static int GetMediumWave() { return PlayerPrefs.GetInt("Medium_Wave", 0); }

    public static void SetHardWave(int index) { PlayerPrefs.SetInt("Hard_Wave", index); }

    public static int GetHardWave() { return PlayerPrefs.GetInt("Hard_Wave", 0); }


}
