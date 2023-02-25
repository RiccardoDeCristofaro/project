using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    // public
    public CharacterController controller;

    // private
    [SerializeField, Range(1f, 10f)] private float speed;
    private float x;
    private float z;

    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            // move player
            x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            z = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            controller.Move((transform.forward * z) - (transform.up * 10f) + (transform.right * x));
        }      
    }
}
