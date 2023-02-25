using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // public
    public Transform player;
    [SerializeField, Range(10f, 200f)] private float cameraSensibility;
    [SerializeField, Range(10f, 90f)] private float motionRange_UPDown;

    // private
    private float leftRight;
    private float upDown;
    private float playerRotationY;
    private float cameraRotationX;

    private void Start()
    {
        playerRotationY = transform.localEulerAngles.y;
    }
    void Update()
    { 
        if (Cursor.lockState == CursorLockMode.Locked)
        {          
            // calculate value to add/subtract to camera/player rotation
            leftRight = Input.GetAxis("Mouse X") * cameraSensibility * Time.deltaTime;
            upDown = Input.GetAxis("Mouse Y") * cameraSensibility * Time.deltaTime;

            // calculate rotation value (degrees) using starting camera/player orientation
            playerRotationY += leftRight;
            cameraRotationX -= upDown;
 
            cameraRotationX = Mathf.Clamp(cameraRotationX, -motionRange_UPDown, motionRange_UPDown);

            // apply new rotation to camera/player
            player.localRotation = Quaternion.Euler(0f, playerRotationY, 0f);
            transform.localRotation = Quaternion.Euler(cameraRotationX, 0f, 0f);
        }
    }
}
