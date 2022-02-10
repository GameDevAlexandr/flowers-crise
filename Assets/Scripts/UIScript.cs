using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    private GameManager gm;
    [HideInInspector] public Text creditsText;
    [HideInInspector] public Text waveText;
    [HideInInspector] public Text strenghtText;
    [HideInInspector] public Text distanceText;
    [HideInInspector] public Text speedText;
    [HideInInspector] public Text powerText;
    [HideInInspector] public Text priceDistanceText;
    [HideInInspector] public Text priceSpeedText;
    [HideInInspector] public Text pricePowerText;
    [HideInInspector] public Text upgradeLimitText;
    [HideInInspector] public Text sellPriceText;
    [HideInInspector] public Text waveComplete;
    [HideInInspector] public Text killsEnemys;
    [HideInInspector] public Text discription1;
    [HideInInspector] public Text discription2;
    [HideInInspector] public Text speedGameText;
    [HideInInspector] public Button radiusUpdateButton;
    [HideInInspector] public Button speedUpdateButton;
    [HideInInspector] public Button powerUpdateButton;
    [HideInInspector] public Button nextDefenderButton1;
    [HideInInspector] public Button nextDefenderButton2;
    [HideInInspector] public Image Icon;
    [HideInInspector] public GameObject rightPanel;
    [HideInInspector] public GameObject losePanel;
    [HideInInspector] public GameObject menuPanel;
    [HideInInspector] public Slider musicSlider;
    [HideInInspector] public Slider soundsSlider;   
    [HideInInspector] public Slider speedSlider;
    public Sprite victoryImage;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        creditsText = GameObject.Find("Credits").GetComponent<Text>();
        waveText = GameObject.Find("Wave").GetComponent<Text>();
        strenghtText = GameObject.Find("Strenght").GetComponent<Text>();
        distanceText = GameObject.Find("DistanceText").GetComponent<Text>();
        speedText = GameObject.Find("SpeedText").GetComponent<Text>();
        powerText = GameObject.Find("PowerText").GetComponent<Text>();
        priceDistanceText = GameObject.Find("PriceDistanceText").GetComponent<Text>();
        priceSpeedText = GameObject.Find("PriceSpeedText").GetComponent<Text>();
        pricePowerText = GameObject.Find("PricePowerText").GetComponent<Text>();
        sellPriceText = GameObject.Find("SellPriceText").GetComponent<Text>();
        waveComplete = GameObject.Find("WaveComplete").GetComponent<Text>();
        killsEnemys = GameObject.Find("KillsEnemys").GetComponent<Text>();
        upgradeLimitText = GameObject.Find("UpgradeLimitText").GetComponent<Text>();
        discription1 = GameObject.Find("Discription1").GetComponent<Text>();
        discription2 = GameObject.Find("Discription2").GetComponent<Text>();
        speedGameText = GameObject.Find("SpeedGameText").GetComponent<Text>();
        radiusUpdateButton = GameObject.Find("RadiusUpdateButton").GetComponent<Button>();
        speedUpdateButton = GameObject.Find("SpeedUpdateButton").GetComponent<Button>();
        powerUpdateButton = GameObject.Find("PowerUpdateButton").GetComponent<Button>();
        nextDefenderButton1 = GameObject.Find("NextDefenderButton1").GetComponent<Button>();
        nextDefenderButton2 = GameObject.Find("NextDefenderButton2").GetComponent<Button>();
        musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        soundsSlider = GameObject.Find("SoundsSlider").GetComponent<Slider>();
        speedSlider = GameObject.Find("SpeedSlider").GetComponent<Slider>();
        Icon = GameObject.Find("ArtIco").GetComponent<Image>();
        rightPanel = GameObject.Find("RightPanel");
        losePanel = GameObject.Find("LosePanel");
        menuPanel = GameObject.Find("MenuPanel");
        menuPanel.SetActive(false);
        losePanel.SetActive(false);
        rightPanel.SetActive(false);
        radiusUpdateButton.interactable = false;
        speedUpdateButton.interactable = false;
        powerUpdateButton.interactable = false;
    } 
    public void updateParam(int paramIndex)
    {
        switch (paramIndex)
        {
            case 1:
                gm.setDefenderParam(1, 0, 0);
                break;
            case 2:
                gm.setDefenderParam(0, 1, 0);
                break;
            case 3:
                gm.setDefenderParam(0, 0, 1);
                break;
        }
        gm.sounds.click.Play();
    }
    public void NextDefenderBuild(int idx)
    {
        gm.BuildNewDefender(idx);
        gm.sounds.click.Play();
    }
    public void PauseButton()
    {
        gm.Pause();
        gm.sounds.click.Play();
    }
    public void Again()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gm.sounds.click.Play();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        gm.Pause();
        gm.sounds.click.Play();
    }
    public void soundsVolume()
    {
        gm.setVolumeSounds();
        gm.sounds.click.Play();
    }
    public void ChangeSpeedGame()
    {
        float speedGame = speedSlider.value;
        if (speedGame == 0)
        {
            gm.setSpeedGame(1);
            speedGameText.text = "x1";
        }
        else
        {
            gm.setSpeedGame(speedGame * 2);
            speedGameText.text = "x"+(speedGame*2).ToString();
        }
    }

}
