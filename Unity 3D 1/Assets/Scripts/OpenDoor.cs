using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    [SerializeField] GameObject target;
    [SerializeField] float doorSpeed;
    [SerializeField] GameObject path;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            if (Vector3.Distance(transform.position, path.transform.position) > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, path.transform.position, doorSpeed * Time.deltaTime);
            }
        }
    }
}
