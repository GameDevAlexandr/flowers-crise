using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemysScript : MonoBehaviour
{
    public int[] flowersTypeNeed;
    [HideInInspector] public bool isDrunk;
    [HideInInspector] public List<Vector3> pathElemeents;
    [HideInInspector] public bool isSatisfy;
    [SerializeField] private int priceOfSatisfy;
    [SerializeField] private float speed;
    [SerializeField] private Image[] UIflowers;
    [SerializeField] private GameObject emptyImage;
    [SerializeField] private GameObject smileImage;
    [SerializeField] private ParticleSystem drunkParticle;
    [SerializeField] private ParticleSystem destroyPS;
    [SerializeField] private bool itsBoss;
    private int[] maxFlowersType;
    private int pathIndex;
    private NavMeshAgent agent;
    private GameManager gm;
    private bool isStarted;
    private float startDrunk;
    private float drunkTime;
    private Animator animator;
    private int bossNeeds;

        
    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        agent.speed = speed;
        isSatisfy = false;
        maxFlowersType = new int[flowersTypeNeed.Length];
        for (int i = 0; i < flowersTypeNeed.Length; i++)
        {
            maxFlowersType[i] = flowersTypeNeed[i];
        }
        pathIndex = 1;
        isStarted = false;
        animator.speed = 0.25f*speed;
    }
    private void Start()
    {
 
    }
    public void startEnemy()
    {
        agent.destination = pathElemeents[pathIndex];        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStarted)
        {
            startEnemy();
            isStarted = true;
        }
        if (agent.remainingDistance == 0)
        {
            if (pathIndex < pathElemeents.Count)
            { 
                startEnemy();
                pathIndex++;
            }
            else
            {
                Finish();
            }
        }
        if (isDrunk)
        {
            if (Time.time - startDrunk > drunkTime)
            {
                agent.speed = speed;
                animator.speed = 0.25f * speed;
                isDrunk = false;
                drunkParticle.gameObject.SetActive(false);
            }
        }
    }
    public void AddFlowers(int flowersCount, int index)
    {
        flowersTypeNeed[index] -= flowersCount;
        if (flowersTypeNeed[index] <= 0)
        {
            flowersTypeNeed[index] = 0;
        }
        float fillIdx = 1 / (float)maxFlowersType[index];
        UIflowers[index].fillAmount = fillIdx * flowersTypeNeed[index];
        bool enemyEmpty = true;
        for (int i = 0; i <flowersTypeNeed.Length; i++)
        {
            if (flowersTypeNeed[i] != 0)
            {
                enemyEmpty = false;
            }
        }
        if (enemyEmpty)
        {
            if (!itsBoss || bossNeeds == 2)
            {
                gm.sounds.satisfy.Play();
                isSatisfy = true;
                agent.speed =speed*2;
                animator.speed = .5f*speed;
                //animator.Play("Victory");
                //destroyPS.Play();
                gm.AddMoney(priceOfSatisfy);
                gm.enemySatisfy(gameObject);
                emptyImage.SetActive(false);
                smileImage.SetActive(true);
                //Destroy(gameObject, 0.7f);
            }
            else
            {
                bossNeeds++;
                UIflowers[bossNeeds-1].gameObject.SetActive(false);
                UIflowers[bossNeeds].gameObject.SetActive(true);
                flowersTypeNeed[bossNeeds] = maxFlowersType[bossNeeds-1];
                maxFlowersType[bossNeeds] = flowersTypeNeed[bossNeeds];
            }
        }
        gm.sounds.attack.Play();
    }
    public void AddWine(float strenght, float time )
    {
        gm.sounds.attack.Play();
        isDrunk = true;
        drunkTime = time;
        startDrunk = Time.time;
        agent.speed = speed / strenght;
        animator.speed = 0.25f * agent.speed;
        drunkParticle.gameObject.SetActive(true);
        
    }
    public void Freeze()
    {
        agent.speed = 0;
    }
    private void Finish()
    {
        if (!isSatisfy)
        {
            gm.FlipPage(1);
        }
        gm.enemySatisfy(gameObject);
        Destroy(gameObject);
    }
}
