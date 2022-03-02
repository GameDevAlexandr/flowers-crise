using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour
{
    [SerializeField] Slider difficultySlider;
    // Start is called before the first frame update
    void Start()
    {
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
    public void StartGameButton()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
