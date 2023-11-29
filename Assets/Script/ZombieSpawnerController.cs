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
    public bool isZombieAttacking = false;
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
            playerScript.ZombieDetected(gameObject);
            LookAtPlayers(other.gameObject);
            isZombieAttacking = true;
        }
    }
    void LookAtPlayers(GameObject target) // zombie colliderine playerlar degdiginde playerlara donmelerini saglar 
    {
        Vector3 pos = transform.position - target.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(pos); // verdimiz noktalardan bize bir donme degeri dondurur aradaki farki verdigimde ne kadar donmesi gerektigini anlar 
        lookRotation.x = 0;
        lookRotation.z = 0; // sadece y de donmesi lazim o yuzden 
        transform.rotation = lookRotation;
    }
    public void ZombieAttackThisCop(GameObject player, GameObject zombie)
    {
        zombieList.Remove(zombie);
        CheckZombieCount();
        playerScript.PlayerGotKilled(player);
        Destroy(zombie);
    }
    void CheckZombieCount()
    {
        if (zombieList.Count <= 0)
        {
            playerScript.AllZombiesKilled();
        }
    }
}
