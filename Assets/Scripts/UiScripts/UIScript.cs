using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static GameDataScript;

public class UIScript : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Toggle muteToggle;
    [SerializeField] private Image scroll;
    [SerializeField] private Image rightHandle;
    [HideInInspector] public Text moneyText;
    [HideInInspector] public Text messageText;
    public GameObject losePanel;
    public GameObject victoryPanel;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        moneyText = GameObject.Find("MoneyText").GetComponent<Text>();
        messageText = GameObject.Find("MessageText").GetComponent<Text>();
        losePanel.SetActive(false);
        victoryPanel.SetActive(false);
        soundSlider.value = soundVolume;
        musicSlider.value = musicVolume;
        muteToggle.isOn = isMute;
        
    } 
    public void Again()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gm.Pause(false);
    }
    public void StartMenu()
    {
        SceneManager.LoadScene(0);
        gm.Pause(false);
    }
    public void ChangeLevel()
    {
        SceneManager.LoadScene(1);
        gm.Pause(false);
    }
    public void ChangeSoundVolume()
    {
        if(!muteToggle.isOn)
        SetSoundVolume(soundSlider.value);
    }
    public void ChangeMusicVolume()
    {
        if(!muteToggle.isOn)
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
    public void IsPause(bool isPause)
    {
        gm.Pause(isPause);
    }
    public void ScrollChange(float strenght)
    {
        scroll.fillAmount += strenght;
        Vector2 handlePosition = rightHandle.rectTransform.localPosition;
        handlePosition.x += scroll.rectTransform.rect.width*strenght;
        rightHandle.rectTransform.localPosition = handlePosition;
    }
    public void Faster(int timeScale)
    {
      Time.timeScale = timeScale;
        gm.sounds.click.Play();
    }

   

}
