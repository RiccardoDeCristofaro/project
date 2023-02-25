using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    /// <summary>
    ///  sensitivity
    /// </summary>
    public float sensX; //  for X axis;
    public float sensY; // Y axis 

    [Tooltip("the viewport of the player cam, oriented by axis movement")]

    /// <summary>
    ///  orientation
    /// </summary>
    public Transform orientation;
    float xRotation;
    float yRotation;

    private void Update()
    {
        // get mouse input;
         float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
         float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        // set limit for the rotation with clamp;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // all the 180° rotation with the full view

        // rotate cam and orientation 
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);// fully rotation 360°
        orientation.rotation = Quaternion.Euler(0,yRotation, 0); // y axis rotation 180°
    }

}
