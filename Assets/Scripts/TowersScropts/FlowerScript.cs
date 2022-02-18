using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerScript : MonoBehaviour
{
    [HideInInspector]public int flowerType;
    [HideInInspector] public Vector3 startPosition;
    [SerializeField] float speed;
    [SerializeField] GameObject[] flowers;
    private Vector3 endPosition;
    private bool isShot;
    private float path;
    private int fType;
    private void Awake()
    {
        isShot = false;
        path = 0;
        for (int i = 0; i < flowers.Length; i++)
        {
            flowers[i] = Instantiate(flowers[i], transform.position, Quaternion.identity);
            flowers[i].transform.parent = gameObject.transform;
            flowers[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isShot)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, path);
            path += speed;
            if (path >= 1)
            {
                isShot = false;
                for (int i = 0; i < flowers.Length; i++)
                {
                    flowers[i].SetActive(false);
                }                
                path = 0;
                Debug.Log("shot");
            }
        }
    }
    public void Shot(Vector3 startPos, Vector3 endPos, int typeFlower)
    {
        startPosition = startPos;
        endPosition = endPos;
        fType = typeFlower;
        flowers[fType].SetActive(true);
        isShot = true;
    }
}
