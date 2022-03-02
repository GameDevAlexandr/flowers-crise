using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameDataScript :MonoBehaviour
{
    
    public static int[] levelRange = new int[10];
    public static int finishLevel;
    public static float musicVolume;
    public static float soundVolume;
    public static UnityEvent SoundEvent = new UnityEvent();
    public static void SetSoundVolume(float volume)
    {
        soundVolume = volume;
        SoundEvent.Invoke();
    }
    public static void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        SoundEvent.Invoke();
    }
}
