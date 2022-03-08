using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class VisiblePath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnDrawGizmos()
    {
        Transform[] pathElements = GetComponentsInChildren<Transform>();
        for (int i = 1; i < pathElements.Length-1; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pathElements[i].position, pathElements[i+1].position);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(pathElements[i].position, 1);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
