using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SwitchCam : MonoBehaviour
{    
    public Camera playerCam;
    public Camera cannonCam;
    public CannonMov cannonMov;
    public CannonFire cannonFire;
    public GameObject player;

    internal bool enable = true;

    public void EnterCannon()
    {
        Debug.Log("Camera");
        cannonMov.enabled = true;
        cannonFire.enabled = true;
        cannonCam.gameObject.SetActive(true);
        playerCam.gameObject.SetActive(false);
        player.SetActive(false);
    }
    public void ExitCannonCam()
    {
        Debug.Log("Exit");
        cannonMov.enabled = false;
        cannonFire.enabled = false;
        cannonCam.gameObject.SetActive(false);
        playerCam.gameObject.SetActive(true);
        player.SetActive(true);
    }
}
