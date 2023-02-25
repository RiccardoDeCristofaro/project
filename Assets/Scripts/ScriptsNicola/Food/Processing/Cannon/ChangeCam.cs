using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCam : MonoBehaviour
{
    public Camera playerCam;
    public Camera cannonCam;
    public CannonMov cannonMov;
    public CannonFire cannonFire;

    internal bool enable = true;

    private void Update()
    {
        LockCursor();
    }
    public void EnterCannon()
    {
        Debug.Log("Camera");
        cannonMov.enabled = true;
        cannonFire.enabled = true;
        cannonCam.gameObject.SetActive(true);
        playerCam.gameObject.SetActive(false);
    }
    public void ExitCannonCam()
    {
        Debug.Log("Exit");
        cannonMov.enabled = false;
        cannonFire.enabled = false;
        cannonCam.gameObject.SetActive(false);
        playerCam.gameObject.SetActive(true);
    }
    private void LockCursor()
    {
        // lock to screen center & hide cursor
        if (Input.GetKeyDown(KeyCode.Mouse1))
            Cursor.lockState = CursorLockMode.Locked;

        // unlock cursor
        if (Input.GetKeyDown(KeyCode.Z))
            Cursor.lockState = CursorLockMode.None;
    }
}
