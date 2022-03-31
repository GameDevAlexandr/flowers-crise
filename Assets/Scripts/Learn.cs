using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Learn : MonoBehaviour
{
    [HideInInspector] public static UnityEvent<bool> learnEvent = new UnityEvent<bool>();
    [HideInInspector] public static UnityEvent<bool> bossActivate = new UnityEvent<bool>();
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private GameObject[] nextObject;
    [SerializeField] private string[] messageHint;
    [SerializeField] private GameObject pointer;
    private Text hintText;
    private int messageCounter = -1;
    private float delaySeconds = 8.0f;
    private int charCounter;
    private char[] stringAtomazer;
    private GameManager gm;

    private void Start()
    {
        EmptyForBuildScript.buildingEvent.AddListener(BuildingEvent);
        TowerScript.upgradeEvent.AddListener(UpgradeEvent);
        TowerScript.boostEvent.AddListener(BoostEvent);
        GameManager.addMoneyEvent.AddListener(AddMoneyEvent);
        TowerUIScript.firstClickEvent.AddListener(FirstClickEvent);
        FlowersMarketScript.fullFlowerEvent.AddListener(FullFlowerEvent);
        hintText = messagePanel.GetComponentInChildren<Text>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void ActivateHint()
    {
        
    }
    private void BuildingEvent()
    {        
        switch (messageCounter)
        {
            case 0:
                SendMessage("Отлично!  Цветов на складе достаточно, что бы обслужить первых покупателей.");
                learnEvent.Invoke(true);
            break;

            case 2:
                SendMessage("Прекрассно! Грузчик из сада доставляет цветы на склад лавки, заполни его, указывая нужный тип цветов.");
                pointer.SetActive(true);
            break;

        }

    }
    private void Update()
    {
        
    }
    IEnumerator MessageOverTime()
    {
        yield return new WaitForSeconds(delaySeconds);
        messagePanel.SetActive(false);        
        if(messageCounter>=nextObject.Length-2 && messageCounter < messageHint.Length)
        {
            //CreateMessage();
        }
        messageCounter++;
        
    }
    private void SendMessage(string message)
    {
        messageCounter++;
        messagePanel.gameObject.SetActive(true);
        stringAtomazer = new char[message.Length];
        stringAtomazer = message.ToCharArray();
        charCounter = 0;
        hintText.text = "";
        bossActivate.Invoke(true);
        StartCoroutine(SpellMessage());
    }
    IEnumerator SpellMessage()
    {
        yield return new WaitForSeconds(0.02f);
        if (charCounter < stringAtomazer.Length) 
        {
            hintText.text += stringAtomazer[charCounter];
            charCounter++;
            StartCoroutine(SpellMessage());
        }
        else
        {
            bossActivate.Invoke(false);
        }
        if (messageCounter == 0)
        {
            learnEvent.Invoke(false);
        }
        if(messageCounter == 4)
        {
            SendMessage("Покупателей слишком много! Небольшое пощрение усилит наших работников и ускорит продажи. " +
                "Включи ее, как только покупатели приблизятся к лавке.");
            pointer.SetActive(true);
        }
    }
    private void BoostEvent()
    {
        if (messageCounter == 5)
        {
            pointer.SetActive(false);
            SendMessage("Вау! Вот это производительность! Дайте работникам немного отдохнуть, и они снова будут готовы на подвиги.");
        }
    }
    private void UpgradeEvent()
    {
        if (messageCounter == 7)
        {
            SendMessage("Великолепно! Тепреь мы точно справимся! Только не забывай пополнять запасы складов и вовремя выдавать премии.");
            learnEvent.Invoke(true);
            pointer.SetActive(false);
            nextObject[1].SetActive(true);
            nextObject[2].SetActive(true);
        }
    }
    private void AddMoneyEvent()
    {
        if (gm.moneyCount == 30 && messageCounter==1)
        {
            learnEvent.Invoke(false);
            SendMessage("О, нет!, наши запасы иссякли, нам нечего предложить поупателям, немедленно постройте сад!");
            nextObject[0].SetActive(true);
        }
        if(gm.moneyCount>=100 && messageCounter == 6)
        {
            learnEvent.Invoke(false);
            SendMessage("Время близится к вечеру, ждите самого большого наплыва покупателей. Нам не справится с ними, если мы не улучшим цветочную лавку.");
            pointer.SetActive(true);
        }
    }
    private void FullFlowerEvent()
    {
        if (messageCounter == 3)
        {
            SendMessage("Вижу, ты в этом деле лучший! Но время стремится к полудню, поэтому будь готов, покупателей станет больше!");
            learnEvent.Invoke(true);
            pointer.SetActive(false);
        }
    }
    private void FirstClickEvent()
    {
        if (messageCounter == -1)
        {
            SendMessage("Первое, что необходимо сделать - это построить цветочную лавку. " +
                         "Нажмите на соответствующую кнопку в меню.");
        }
    }
}
