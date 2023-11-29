using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnerController : MonoBehaviour
{
    [SerializeField] float playerSpeed = 5;
    [SerializeField] GameObject playerObject;
    float xSpeed;
    Animator animator;
    bool isPlaying = false;
    [SerializeField] List<GameObject> playerList = new List<GameObject>();
    float passTime = 0;
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }


    void Update()
    {
        PlayerMovement();
        GetTime();
    }

    void PlayerMovement()
    {
        if (isPlaying)
        {
            return;
        }
        float touchX = 0;
        float newXValue;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) // dokunma varsa touchCount artacak dokunma sekli GetTouchPhase == hareket seklinde ise 
        {
            xSpeed = 250f;
            touchX = Input.GetTouch(0).deltaPosition.x / Screen.width; // dokundugum yerin x de pozisyonunu aliyorum 
        }
        else if (Input.GetMouseButton(0)) // editorde hareket icin 
        {
            xSpeed = 350f;
            touchX = Input.GetAxis("Mouse X");
        }
        newXValue = transform.position.x + xSpeed * touchX * Time.deltaTime;
        newXValue = Mathf.Clamp(newXValue, -4.20f, 4.50f);
        Vector3 playerNewPosition = new Vector3(newXValue, transform.position.y, transform.position.z + playerSpeed * Time.deltaTime);
        transform.position = playerNewPosition;
        // her framede gecen sureyle carpmassam daha guclu cihazlarda frame daha hizli akar bu yuzden karakter daha hizliz hareket eder bu farkliligi engellemek icin carpiyorum 
        //PlayerIsRun();
    }
    //void PlayerIsRun()
    //{
    //    animator.SetBool("IsRun", true);
    //    animator.SetBool("IsIdle", false);
    //}
    public void SpawnPlayer(int gateValue, GateType gateType) // kapidan gecince + veya * islemine gore player spawn etme 
    {
        if (passTime < 0.3f) // yan yana 2 gate varken birinden gecip direkt digerine giremesin diye
        {
            return;
        }
        passTime = 0;

        if (gateType == GateType.PlusGate)
        {
            for (int i = 0; i < gateValue; i++)
            {
                GameObject newPlayerObject = Instantiate(playerObject, GetPlayerPosition(), Quaternion.identity, transform); // son yazdigim transform PlayerSpawner objesinin childi olarak olusturulmasi icin
                playerList.Add(newPlayerObject);
            }
        }
        if (gateType == GateType.MultiplicationGate)
        {
            int newAmount = playerList.Count * gateValue - playerList.Count; // listeyi bu yuzden yaptim listedeki eleman sayisini value ile carparak yeni eleman sayisi kadar donguyu donduruyorum 
            for (int i = 0; i < newAmount; i++)
            {
                GameObject newPlayerObject = Instantiate(playerObject, GetPlayerPosition(), Quaternion.identity, transform); // son yazdigim transform PlayerSpawner objesinin childi olarak olusturulmasi icin
                playerList.Add(newPlayerObject);
            }
        }
    }
    Vector3 GetPlayerPosition()
    {
        Vector3 position = Random.onUnitSphere * 0.1f; // random olusturmayi 0.1 alanlik bolge icinde yap diyoruz 
        Vector3 newPos = transform.position + position;
        return newPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {
            isPlaying = true;
        }
    }
    public void PlayerGotKilled(GameObject playerGO) // engellere deyen playerlari oldurur 
    {
        playerList.Remove(playerGO);
        Destroy(playerGO);
    }
    void GetTime()
    {
        passTime += Time.deltaTime;
    }
    public void ZombieDetected()
    {
        isPlaying = true;
    }
}
