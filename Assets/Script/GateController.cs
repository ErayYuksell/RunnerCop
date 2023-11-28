using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GateController : MonoBehaviour
{
    GameObject playerObject;
    PlayerSpawnerController playerScript;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] int increaseAmount;
    bool isTouched = false;
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("PlayerSpawner");
        playerScript = playerObject.GetComponent<PlayerSpawnerController>();
        GateStart();
    }

   
    void Update()
    {
        
    }
    void GateStart()
    {
        text.text = "+" + increaseAmount;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerSpawner") && isTouched == false)
        {
            isTouched = true;
            MultiPlayer();
        }
    }
    void MultiPlayer()
    {
        playerScript.SpawnPlayer(increaseAmount);
    }
}
