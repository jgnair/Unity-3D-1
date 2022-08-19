using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfo : MonoBehaviour
{

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Terrain" || col.gameObject.tag == "Bullet" || col.gameObject.tag == "Player" || col.gameObject.tag == "Target")
        {
            Destroy(gameObject);
            if(col.gameObject.tag == "Target")
            {
                Destroy(col.gameObject);
            }
        }
    }
}
