using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemysScript : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameManager gm;
    private GameObject[] target;
    [SerializeField] private float maxHealth;
    //[SerializeField] private ParticleSystem smokeEffect;
    //[SerializeField] private ParticleSystem damageEffect;
    //[SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private int priceOfDeath;
    [SerializeField] private Image healthIco;
    [SerializeField] private int redFlowers; 
    [SerializeField] private int blueFlowers; 
    [SerializeField] private int yellowFlowers; 
    public float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>(); 
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectsWithTag("EnemyFinish");
        int targetIndex = Random.Range(0, target.Length);
        agent.destination = target[targetIndex].transform.position;
        currentHealth = maxHealth;               
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddDamage(int redF, int blueF, int YellowF)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            healthIco.fillAmount = currentHealth / maxHealth;
            damageEffect.startSize = 5 + damage/5;
            damageEffect.Play();
            if (currentHealth <= maxHealth / 2)
            {
                smokeEffect.Play();
            }
        }
        if(currentHealth<=0)
        {
            Death();
        }
    }
    private void Death()
    {
        explosionEffect.Play();
        gm.setCredits(priceOfDeath);
        gm.killsCounter++;
        Debug.Log(gm.killsCounter);
        gm.sounds.explosion.Play();
        Destroy(gameObject, 1);        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyFinish")
        {
            gm.DamageToBase(currentHealth);
            Destroy(gameObject);
        }
        
    }
}
