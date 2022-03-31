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
    [SerializeField] private GameObject previewPanel;
    [SerializeField] private Button[] levelMarks;    
    // Start is called before the first frame update
    void Start()
    {
        soundSlider.value = soundVolume;
        musicSlider.value = musicVolume;
        muteToggle.isOn = isMute;
        setDifficulty();
        for (int i = 0; i < levelRange.Length; i++)
        {
            if (levelRange[i] > 0 && i<levelMarks.Length-1)
            {
                levelMarks[i].interactable = true;
            }
        }
        if (previewPanel)
        {
            previewPanel.SetActive(loadPreview);
        }
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
    public void NoLoadPreview()
    {
        loadPreview = false;
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
