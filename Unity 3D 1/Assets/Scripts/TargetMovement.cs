using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{

    [SerializeField] GameObject[] paths;
    int currentPathIndex = 0;

    [SerializeField] float targetSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, paths[currentPathIndex].transform.position) < .1f)
        {
            currentPathIndex++;
            if(currentPathIndex >= paths.Length)
            {
                currentPathIndex = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, paths[currentPathIndex].transform.position, targetSpeed * Time.deltaTime);
    }
}
