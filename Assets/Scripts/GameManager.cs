using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GameDataScript;

public class GameManager : MonoBehaviour
{
    private UIScript ui;
    private TowerUIScript selectedTower;
    [SerializeField] private int pagesCount;
    [SerializeField] private AudioMixerGroup audioMixer;
    public GameObject emptyForTower;
    public int moneyCount;
    [HideInInspector] public List<FlowersMarketScript> marketInScene;
    [HideInInspector] public List<GreenHouseScript> greenHouseinScene;
    [HideInInspector] public List<GameObject> enemys;
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
}
