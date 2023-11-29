using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnerController : MonoBehaviour
{
    [SerializeField] GameObject zombieObject;
    [SerializeField] int zombieCount;
    [SerializeField] List<GameObject> zombieList = new List<GameObject>();
    GameObject playerObject;
    PlayerSpawnerController playerScript;
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("PlayerSpawner");
        playerScript = playerObject.GetComponent<PlayerSpawnerController>();
        SpawnZombies(zombieCount);
    }


    void Update()
    {

    }
    public void SpawnZombies(int zombieCount)
    {
        for (int i = 0; i < zombieCount; i++)
        {
            Quaternion zombieRotation = Quaternion.Euler(new Vector3(0, 180, 0));
            GameObject zombie = Instantiate(zombieObject, GetZombiePosition(), zombieRotation, transform);
            zombieList.Add(zombie);
        }
    }
    public Vector3 GetZombiePosition()
    {
        Vector3 pos = Random.insideUnitSphere * 0.1f;
        Vector3 newPos = transform.position + pos;
        return newPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<BoxCollider>().enabled = false; // bir kere degdikten sorna diger playerlar tekrar degmesin diye kapadik 
            playerScript.ZombieDetected();
        }
    }
}
