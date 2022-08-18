using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSphereInfo : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Terrain")
        {
            rb.useGravity = false;
            rb.velocity = new Vector3(0, 0, 0);
        }
        else if(col.gameObject.tag == "Player" || col.gameObject.tag == "Bullet")
        {
            Destroy (gameObject);
        }
    }
}
