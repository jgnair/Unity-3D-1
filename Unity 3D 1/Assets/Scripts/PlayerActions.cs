using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float jumpSpeed = 2f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firepoint;
    [SerializeField] Camera cam;
    [SerializeField] float TPSphereTimer;
    
    Transform camT;
    Vector3 destination;
    Vector2 input;


    bool TPSphere;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camT = cam.transform;
        TPSphere = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Cursor.visible == true)
        {
            Cursor.visible = false;
        }
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);
        transform.position += (camT.forward * input.y + camT.right * input.x) * Time.deltaTime * movementSpeed;

        if (Input.GetButtonDown("Jump") && Grounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= TPSphereTimer)
        {
            TPSphereTimer = Time.time + 1 / 10;
            TPSPhereAbility();
        }
    }

    bool Grounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.1f, ground);
    }

    void TPSPhereAbility()
    {
        if (TPSphere)
        {
            TPSphere = false;
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                destination = hit.point;
            }
            else
            {
                destination = ray.GetPoint(1000);
            }
            createTPSphere(firepoint);
        }
        else
        {
            transform.position = new Vector3(projectile.transform.position.x, projectile.transform.position.y + 0.5f, projectile.transform.position.z);
            TPSphere = true;
        }
    }

    void createTPSphere(Transform firePoint)
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
    }

}
