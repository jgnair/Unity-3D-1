using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSphereInfo : MonoBehaviour
{
    Rigidbody rb;
    bool collision;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collision = false;
    }

    void OnCollissionEnter(Collision col)
    {
        if((col.gameObject.tag == "Ground" || col.gameObject.tag == "Wall") && !collision)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
        else if(col.gameObject.tag == "Player")
        {
            collision = true;
            Destroy (gameObject);
        }
    }
}
