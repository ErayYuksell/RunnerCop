using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    GameObject playerSpawnerObject;
    GameObject zombieSpawnerObject;
    ZombieSpawnerController zombieSpawnScript;
    bool isZombieAlive = true;
    void Start()
    {
        playerSpawnerObject = GameObject.FindGameObjectWithTag("PlayerSpawner");
        zombieSpawnerObject = GameObject.FindGameObjectWithTag("ZombieSpawner");
        zombieSpawnScript = zombieSpawnerObject.GetComponent<ZombieSpawnerController>();
    }


    void FixedUpdate() // moveT kullandigim icin fiziksel olaylarda kullanilan bu update cesidi daha iyi
    {
        ZombieMovement();
    }
    void ZombieMovement()
    {
        if (zombieSpawnScript.isZombieAttacking == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerSpawnerObject.transform.position, Time.fixedDeltaTime * 1.5f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && isZombieAlive == true)
        {
            isZombieAlive = false;
            zombieSpawnScript.ZombieAttackThisCop(collision.gameObject, gameObject);
        }
    }
}
