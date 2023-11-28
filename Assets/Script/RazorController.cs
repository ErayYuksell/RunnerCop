using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazorController : MonoBehaviour
{
    GameObject playerObject;
    PlayerSpawnerController playerScript;
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("PlayerSpawner");
        playerScript = playerObject.GetComponent<PlayerSpawnerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerScript.PlayerGotKilled(other.gameObject);
        }
    }
}
