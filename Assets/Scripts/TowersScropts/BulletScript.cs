using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public Vector3 endPosition;
    [HideInInspector] public float speed;
    [SerializeField]  private GameObject[] bulletsType;
    private int curBulletType;
    private float progress;// Start is called before the first frame update
    private void Awake()
    {
        startPosition = transform.position;
        for (int i = 0; i < bulletsType.Length; i++)
        {
            bulletsType[i] = GameObject.Instantiate(bulletsType[i], transform.position, Quaternion.identity);
            bulletsType[i].transform.parent = gameObject.transform;
            bulletsType[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, progress);
            progress += speed;
            if (progress >= 1)
            {
                transform.position = startPosition;
                gameObject.SetActive(false);
                bulletsType[curBulletType].SetActive(false);
            }
        }
    }
    public void shot(Vector3 endPos, int bulletType)
    {
        curBulletType = bulletType;
        bulletsType[bulletType].SetActive(true);
        endPosition = endPos;
        progress = 0;
    }
}
