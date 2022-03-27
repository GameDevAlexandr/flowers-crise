using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GameDataScript;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    
    
    public GameObject emptyForTower;
    public int moneyCount;
    [HideInInspector] public static UnityEvent addMoneyEvent = new UnityEvent();
    [HideInInspector] public Sounds sounds;
    [HideInInspector] public List<FlowersMarketScript> marketInScene;
    [HideInInspector] public List<GreenHouseScript> greenHouseinScene;
    [HideInInspector] public List<GameObject> enemys;
    [SerializeField] private int pagesCount;
    [SerializeField] private int TwoStar;
    [SerializeField] private int ThreeStar;
    [SerializeField] private bool noSpawnOnStart;
    private UIScript ui;
    private TowerUIScript selectedTower;
    private List<Spawner> spawners;
    private int counterPage;
    private int spawnNumber;
    private float timeScaler;
    
    private void Start()
    {
        counterPage = 0;
        sounds = GameObject.Find("Sounds").GetComponent<Sounds>();
        GameObject[] spaws = GameObject.FindGameObjectsWithTag("Spawner");
        spawners = new List<Spawner>();
        for (int i = 0; i < 3; i++)
        {
            spawners.Add(null);
        }
        for (int i = 0; i < spaws.Length; i++)
        {
            Spawner sp = spaws[i].GetComponent<Spawner>();
            spawners[sp.spawnNumber] = sp;
        }
        marketInScene = new List<FlowersMarketScript>();
        greenHouseinScene = new List<GreenHouseScript>();
        ui = GameObject.Find("UI").GetComponent<UIScript>();
        ui.moneyText.text = moneyCount.ToString();
        SetSoundVolume(soundVolume);
        SetMusicVolume(soundVolume);
        if (!noSpawnOnStart)
        {
            spawners[0].StartSpawner();
        }
        //Pause(true);
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
        addMoneyEvent.Invoke();
    }
    public void FlipPage(int count)
    {
        counterPage += count;
        ui.ScrollChange((float)count/pagesCount);
        //ui.pagesText.text = pagesCount.ToString();
        sounds.bookOfComplaint.Play();
        if (counterPage>= pagesCount)
        {
            ui.losePanel.SetActive(true);
            sounds.backGroundMusic.Stop();
            sounds.loseMusic.Play();
            Pause(true);
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
            timeScaler = Time.timeScale;
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = timeScaler;
        }
    }
    public void enemySatisfy(GameObject enemy)
    {
        enemys.Remove(enemy);
        if (enemys.Count == 0 && pagesCount > 0 )
        {
            spawners.Remove(spawners[0]);
            if (spawners.Count == 0)
            {
                Victory();
            }
            else
            {
                sounds.backGroundMusic.Stop();
                if (spawnNumber == 0)
                {                   
                    sounds.backGroundMusic = sounds.wawe2;
                }
                else
                {
                    sounds.backGroundMusic = sounds.wawe3;                   
                }
                sounds.backGroundMusic.Play();
                spawners[0].StartSpawner();
                spawnNumber++;
            }
        }
    }
    public void SendMessage(string message,float delayTime)
    {
        ui.messageText.text = message;
        StartCoroutine(MessageOverTime(delayTime));
    }
    private void Victory()
    {
        ui.victoryPanel.SetActive(true);
        sounds.backGroundMusic.Stop();
        sounds.victoryMusic.Play();
        GameDataScript.levelRange[SceneManager.GetActiveScene().buildIndex - 1] = 1;
        if (counterPage > ThreeStar && counterPage<=TwoStar)
        {
            GameDataScript.levelRange[SceneManager.GetActiveScene().buildIndex - 1] = 2;
        }
        else if(counterPage <= ThreeStar)
        {
            GameDataScript.levelRange[SceneManager.GetActiveScene().buildIndex - 1] = 3;
        }
        Pause(true);
        LevelRange();
    }
    IEnumerator MessageOverTime(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        ui.messageText.text = "";
    }
}
