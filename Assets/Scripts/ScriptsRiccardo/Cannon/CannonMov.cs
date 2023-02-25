using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMov : MonoBehaviour
{   
    [Header("Cam settings")]
    [Tooltip("Min = 10\nMax = 400")]
    [SerializeField, Range(10f, 200f)] private float cameraSensibility;
   
    private float CamRot;

    [Header("range settings")]   
    [Tooltip("Min = 10\nMax = 45")]
    [SerializeField, Range(10f, 90f)] private float motionRange_LeftRight;
       
    private float LeftRightNew;

    private void Start()
    {
        CamRot = 0;
    }

    private void Update()
    {    
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            // inputs          
            LeftRightNew = -Input.GetAxisRaw("Mouse X") * cameraSensibility * Time.deltaTime;
           
            CamRot -= LeftRightNew;

            CamRot = Mathf.Clamp(CamRot, -motionRange_LeftRight, motionRange_LeftRight);
         
            //  Player.localRotation = Quaternion.Euler(0f, 0f, PlayRot); // revisionare per capire 
            transform.localRotation = Quaternion.Euler(0f, CamRot, 0f);
        }
    }
}
