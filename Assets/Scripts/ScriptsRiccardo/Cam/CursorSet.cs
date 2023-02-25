using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSet : MonoBehaviour
{
    public Texture2D cursor;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void OnGUI()
    {
        
        Cursor.visible = true;
        
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware); // cursor set
    }
}
