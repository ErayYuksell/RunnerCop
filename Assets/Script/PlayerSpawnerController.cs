using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnerController : MonoBehaviour
{
    [SerializeField] float playerSpeed = 5;
    float xSpeed;
    Animator animator;
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }


    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
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
        PlayerIsRun();
    }
    void PlayerIsRun()
    {
        animator.SetBool("IsRun", true);
        animator.SetBool("IsIdle", false);
    }
}
