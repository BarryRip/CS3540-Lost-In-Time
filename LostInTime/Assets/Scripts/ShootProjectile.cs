using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;

    public float projectileSpeed = 100;

    public static GameObject currentProjectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        currentProjectilePrefab = projectilePrefab;
    }

    // Update is called once per frame
    void Update()
    {

            if (Input.GetButtonDown("Fire1"))
            {
                GameObject projectile = Instantiate(projectilePrefab, transform.position + transform.forward, transform.rotation) as GameObject;

                Rigidbody rb = projectile.GetComponent<Rigidbody>();

                rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);

                projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);


            }
    }
}
