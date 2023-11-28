using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GateType { PlusGate, MultiplicationGate } // class disina yazmassam PlayerSpawnController dan ulasamassin
public class GateController : MonoBehaviour
{
    GameObject playerObject;
    PlayerSpawnerController playerScript;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] int gateValue;
   
    public GateType gateType;
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
        if (gateType == GateType.PlusGate)
        {
            text.text = "+" + gateValue;
        }
        if (gateType == GateType.MultiplicationGate)
        {
            text.text = "X" + gateValue;
        }
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
        playerScript.SpawnPlayer(gateValue, gateType);
    }
}
