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
                SendMessage("�������!  ������ �� ������ ����������, ��� �� ��������� ������ �����������.");
                learnEvent.Invoke(true);
            break;

            case 2:
                SendMessage("����������! ������� �� ���� ���������� ����� �� ����� �����, ������� ���, �������� ������ ��� ������.");
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
            SendMessage("����������� ������� �����! ��������� �������� ������ ����� ���������� � ������� �������. " +
                "������ ��, ��� ������ ���������� ����������� � �����.");
            pointer.SetActive(true);
        }
    }
    private void BoostEvent()
    {
        if (messageCounter == 5)
        {
            pointer.SetActive(false);
            SendMessage("���! ��� ��� ������������������! ����� ���������� ������� ���������, � ��� ����� ����� ������ �� �������.");
        }
    }
    private void UpgradeEvent()
    {
        if (messageCounter == 7)
        {
            SendMessage("�����������! ������ �� ����� ���������! ������ �� ������� ��������� ������ ������� � ������� �������� ������.");
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
            SendMessage("�, ���!, ���� ������ �������, ��� ������ ���������� ����������, ���������� ��������� ���!");
            nextObject[0].SetActive(true);
        }
        if(gm.moneyCount>=100 && messageCounter == 6)
        {
            learnEvent.Invoke(false);
            SendMessage("����� �������� � ������, ����� ������ �������� ������� �����������. ��� �� ��������� � ����, ���� �� �� ������� ��������� �����.");
            pointer.SetActive(true);
        }
    }
    private void FullFlowerEvent()
    {
        if (messageCounter == 3)
        {
            SendMessage("����, �� � ���� ���� ������! �� ����� ��������� � �������, ������� ���� �����, ����������� ������ ������!");
            learnEvent.Invoke(true);
            pointer.SetActive(false);
        }
    }
    private void FirstClickEvent()
    {
        if (messageCounter == -1)
        {
            SendMessage("������, ��� ���������� ������� - ��� ��������� ��������� �����. " +
                         "������� �� ��������������� ������ � ����.");
        }
    }
}
