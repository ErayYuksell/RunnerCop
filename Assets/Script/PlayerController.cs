using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject bulletObject;
    [SerializeField] Transform bulletSpawnTransform;
    [SerializeField] int bulletSpeed = 13;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void StartShooting()
    {
        Shoot();
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletObject, bulletSpawnTransform.position, Quaternion.identity);
        Rigidbody bulletRB = bulletObject.GetComponent<Rigidbody>();
        bulletRB.velocity = transform.forward * bulletSpeed;
    }
}
