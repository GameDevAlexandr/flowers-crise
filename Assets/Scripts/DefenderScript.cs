using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderScript : MonoBehaviour
{
    public Sprite icon;
    public float damage;
    public float fireSpeed;
    public float radius;
    public int price;
    public int radiusUpdateprice;
    public int danageUpdateprice;
    public int speedUpdateprice;
    public int sellPrice;
    public int upgradeLimit;
    public int nextDefenderIndex1;
    public int nextDefenderIndex2;
    public string discription;
    public ParticleSystem radiusSphere;
    [SerializeField] private LayerMask enemyLayer;
    //[SerializeField] private ParticleSystem bullet;
    [SerializeField] private ParticleSystem shotEffect;
    private GameManager gm;
    private float startTimeofShot;
    private AudioSource shotAudio;
    void Start()
    {
        startTimeofShot = Time.time;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        setRadius();
        sellPrice = price / 2;
        radiusSphere.gameObject.SetActive(false);
        shotAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTimeofShot >= 60 / fireSpeed)
        {
            Fire();
            startTimeofShot = Time.time;
        }
      
    }
    private void Fire()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        for (int i = 0; i < hitColliders.Length; i++)
        {    
            if(hitColliders[i].tag == "Enemy"&& hitColliders[i].GetComponent<EnemysScript>().currentHealth>0)
            {
                transform.LookAt(hitColliders[i].transform.position);
                hitColliders[i].GetComponent<EnemysScript>().AddDamage(damage);
                shotEffect.Play();
                shotAudio.Play();
                break;
            }           
        }
    }
    public void SellCalculation(int updatePrice)
    {
        sellPrice += updatePrice / 2;
    }
    public void setRadius()
    {
        ParticleSystem.ShapeModule shape = radiusSphere.shape;
        shape.radius = radius;
    }
}
