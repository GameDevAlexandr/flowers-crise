using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    private GameManager gm;
    [HideInInspector] public Text moneyText;
    [HideInInspector] public Text pagesText;
    [HideInInspector] public GameObject losePanel;
    [HideInInspector] public GameObject victoryPanel;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        moneyText = GameObject.Find("MoneyText").GetComponent<Text>();
        pagesText = GameObject.Find("PagesText").GetComponent<Text>();
        losePanel = GameObject.Find("LosePanel");
        victoryPanel = GameObject.Find("VictoryPanel");
        losePanel.SetActive(false);
        victoryPanel.SetActive(false);
    } 
    public void Again()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ChangeLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void OpenMenu()
    {

    }
   

}
