using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            transform.eulerAngles += speed * new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
    }
}
