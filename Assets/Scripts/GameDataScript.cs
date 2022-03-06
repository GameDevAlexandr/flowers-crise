using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class GameDataScript :MonoBehaviour
{
    
    public static int[] levelRange = new int[10];
    public static int finishLevel;
    public static float musicVolume;
    public static float soundVolume;
    public static bool isMute;
    public static AudioMixerGroup audioMixer = Resources.Load<AudioMixerGroup>("AudioMixer");
    public static UnityEvent changeRange = new UnityEvent();
    public static void SetSoundVolume(float volume)
    {
        soundVolume = volume;
        audioMixer.audioMixer.SetFloat("Sounds", soundVolume);
    }
    public static void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        audioMixer.audioMixer.SetFloat("Music", musicVolume);
    }
    public static void LevelRange()
    {
        changeRange?.Invoke();
    }

}
