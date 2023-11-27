using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] Vector3 offSet;
    [SerializeField] GameObject Target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CameraMovement();
    }
    void CameraMovement()
    {
        transform.position = Target.transform.position + offSet;
    }
}
