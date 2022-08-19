using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    [SerializeField] GameObject target;
    [SerializeField] float doorSpeed;
    [SerializeField] GameObject path;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            while(transform.position != path.transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, path.transform.position, doorSpeed * Time.deltaTime);
            }
        }
    }
}
