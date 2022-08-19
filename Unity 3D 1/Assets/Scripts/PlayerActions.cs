using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerActions : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float jumpSpeed = 2f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] Transform firepoint;
    [SerializeField] GameObject TPSphere;
    [SerializeField] float TPSphereFirerate;
    float TPSphereTimer = 0f;
    GameObject TPSphereObj;
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletFirerate;
    float bulletTimer = 0f;
    GameObject bulletObj;
    [SerializeField] TextMeshProUGUI ammo;
    [SerializeField] TextMeshProUGUI TPSphereCooldown;
    [SerializeField] int maxAmmo;
    int curAmmo;

    [SerializeField] Camera cam;
    Transform camT;
    Vector3 destination;
    Vector2 input;


    bool TPSphereUse;

    // Start is called before the first frame update
    void Start()
    {
        curAmmo = maxAmmo;
        rb = GetComponent<Rigidbody>();
        camT = cam.transform;
        TPSphereUse = true;
        ammo.text = curAmmo + "/" + maxAmmo;
        TPSphereCooldown.text = "Ready";
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

        if (Input.GetKeyDown("space") && Grounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
        }

        if (Input.GetKeyDown("e") && Time.time >= TPSphereTimer)
        {
            TPSPhereAbility();
        }

        if(TPSphereTimer > Time.time)
        {
            TPSphereCooldown.text = ((int)(TPSphereTimer-Time.time)).ToString();
        }
        else
        {
            TPSphereCooldown.text = "Ready";
        }

        if (Input.GetButtonDown("Fire1") && curAmmo != 0 && Time.time >= bulletTimer)
        {
            bulletTimer = Time.time + bulletFirerate;
            FireBullet();
            curAmmo--;
            ammo.text = curAmmo + "/" + maxAmmo;
        }

        if(Input.GetKeyDown("r") && curAmmo != maxAmmo)
        {
            curAmmo = maxAmmo;
            ammo.text = curAmmo + "/" + maxAmmo;
        }
    }

    bool Grounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.1f, ground);
    }

    void FireBullet()
    {
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
        createBullet(firepoint, destination - firepoint.position);
    }

    void TPSPhereAbility()
    {
        if (TPSphereUse)
        {
            TPSphereUse = false;
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
            TPSphereTimer = Time.time + TPSphereFirerate;
            transform.position = new Vector3(TPSphereObj.transform.position.x, TPSphereObj.transform.position.y, TPSphereObj.transform.position.z);
            Destroy(TPSphereObj);
            TPSphereUse = true;
        }
    }

    void createTPSphere(Transform firePoint)
    {
        TPSphereObj = Instantiate(TPSphere, firePoint.position, Quaternion.identity) as GameObject;
        TPSphereObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
    }

    void createBullet(Transform firePoint, Vector3 dir)
    {

        bulletObj = Instantiate(bullet, firePoint.position, Quaternion.Euler(cam.transform.rotation.eulerAngles.x + 90, cam.transform.rotation.eulerAngles.y, cam.transform.rotation.eulerAngles.z)) as GameObject;
        bulletObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
        
    }
}
