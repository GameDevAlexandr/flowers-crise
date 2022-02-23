using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GameDataScript;

public class GameManager : MonoBehaviour
{
    
    
    public GameObject emptyForTower;
    public int moneyCount;
    [HideInInspector] public List<FlowersMarketScript> marketInScene;
    [HideInInspector] public List<GreenHouseScript> greenHouseinScene;
    [HideInInspector] public List<GameObject> enemys;
    [SerializeField] private int pagesCount;
    [SerializeField] private AudioMixerGroup audioMixer;
    private UIScript ui;
    private TowerUIScript selectedTower;
    private float curSoundsVolume;
    private void Start()
    {
        marketInScene = new List<FlowersMarketScript>();
        greenHouseinScene = new List<GreenHouseScript>();
        ui = GameObject.Find("UI").GetComponent<UIScript>();
        ui.moneyText.text = moneyCount.ToString();
        ui.pagesText.text = pagesCount.ToString();
    }
    private void Update()
    {
        if (enemys.Count == 0)
        {
            ui.victoryPanel.SetActive(true);
        }
    }
    public void onToutchEvent(Vector3 position)
    {

        RaycastHit rcHit = new RaycastHit();
        if (Physics.Raycast(Camera.main.ScreenPointToRay(position), out rcHit))
        {
            if (rcHit.transform.tag == "Tower")
            {
                if(selectedTower!=null && selectedTower.transform != rcHit.transform)
                {
                    selectedTower.ActivateUI(false);
                }
                selectedTower = rcHit.transform.GetComponent<TowerUIScript>();
                selectedTower.ActivateUI(true);
            }
            else if (selectedTower != null && !EventSystem.current.IsPointerOverGameObject())
            {                
                selectedTower.ActivateUI(false);
            }
        }
    }
    public void AddMoney(int money)
    {
        moneyCount += money;
        ui.moneyText.text = moneyCount.ToString();
    }
    public void FlipPage(int count)
    {
        pagesCount -= count;
        ui.pagesText.text = pagesCount.ToString();
        if (pagesCount <= 0)
        {
            ui.losePanel.SetActive(true);
        }
    }
    public void SetAudioVolume()
    {
        audioMixer.audioMixer.SetFloat("Music", musicVolume);
        audioMixer.audioMixer.SetFloat("Sounds", soundVolume);
    }
    public void updateTowers()
    {
        if (greenHouseinScene.Count != 0)
        {
            for (int i = 0; i < greenHouseinScene.Count; i++)
            {
                greenHouseinScene[i].GetTowers();
            }
        }
    }
    public void Pause(bool isPause)
    {
        if (isPause)
        {
            Time.timeScale = 0;
            musicVolume *= 0.5f;
            curSoundsVolume = soundVolume;
            soundVolume = 0;
            SetAudioVolume();
        }
        else
        {
            Time.timeScale = 1;
            musicVolume *= 2;
            soundVolume = curSoundsVolume;
            SetAudioVolume();
        }
    }
}
