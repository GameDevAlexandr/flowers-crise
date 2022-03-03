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
    [HideInInspector] public Sounds sounds;
    [HideInInspector] public List<FlowersMarketScript> marketInScene;
    [HideInInspector] public List<GreenHouseScript> greenHouseinScene;
    [HideInInspector] public List<GameObject> enemys;
    [SerializeField] private int pagesCount;
    private UIScript ui;
    private TowerUIScript selectedTower;
    
    private void Start()
    {
        sounds = GameObject.Find("Sounds").GetComponent<Sounds>();
        marketInScene = new List<FlowersMarketScript>();
        greenHouseinScene = new List<GreenHouseScript>();
        ui = GameObject.Find("UI").GetComponent<UIScript>();
        ui.moneyText.text = moneyCount.ToString();
        ui.pagesText.text = pagesCount.ToString();
        SetSoundVolume(soundVolume);
        SetMusicVolume(soundVolume);
        //SoundEvent.AddListener(SetAudioVolume);
    }
    private void Update()
    {

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
        sounds.bookOfComplaint.Play();
        if (pagesCount <= 0)
        {
            ui.losePanel.SetActive(true);
            sounds.backGroundMusic.Stop();
            sounds.loseMusic.Play();
        }
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
        sounds.click.Play();
        if (isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void enemySatisfy(GameObject enemy)
    {
        enemys.Remove(enemy);
        if (enemys.Count == 0 && pagesCount > 0)
        {
            ui.victoryPanel.SetActive(true);
            sounds.backGroundMusic.Stop();
            sounds.victoryMusic.Play();
        }
    }
}
