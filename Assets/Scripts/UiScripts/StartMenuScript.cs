using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameDataScript;

public class StartMenuScript : MonoBehaviour
{
    [SerializeField] Slider difficultySlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Toggle muteToggle;
    // Start is called before the first frame update
    void Start()
    {
        soundSlider.value = soundVolume;
        musicSlider.value = musicVolume;
        muteToggle.isOn = isMute;
        setDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setDifficulty()
    {
        //difficulty = (int)difficultySlider.value;
    }
    public void StartGameButton(int idx)
    {
        SceneManager.LoadScene(idx);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void ChangeSoundVolume()
    {
        if (!muteToggle.isOn)
            SetSoundVolume(soundSlider.value);
    }
    public void ChangeMusicVolume()
    {
        if (!muteToggle.isOn)
            SetMusicVolume(musicSlider.value);
    }
    public void Mute()
    {
        isMute = muteToggle.isOn;
        if (muteToggle.isOn)
        {
            SetSoundVolume(-80);
            SetMusicVolume(-80);
        }
        else
        {
            ChangeSoundVolume();
            ChangeMusicVolume();
        }
    }
}
