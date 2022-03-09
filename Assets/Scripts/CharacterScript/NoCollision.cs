using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NoCollision : MonoBehaviour
{
    private NavMeshObstacle obstacle;
    private float stopedTime;
    private Vector3 movedDistance;

    private void Awake()
    {
        obstacle = GetComponent<NavMeshObstacle>();
        stopedTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - stopedTime > 0.3f)
        {
            movedDistance = transform.position;
            stopedTime = Time.time;
            StartCoroutine(checkCollision());
        }
    }
    IEnumerator checkCollision()
    {
        
        yield return new WaitForSeconds(0.2f);
        if (Mathf.Abs(Vector3.Distance(movedDistance, transform.position)) < 0.1f)
        {
            obstacle.enabled = true;
            Debug.Log(Vector3.Distance(movedDistance, transform.position));
        }
        else
        {
            obstacle.enabled = false;
        }
    }
}
