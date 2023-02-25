using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OpenRecipeBook : MonoBehaviour
{
    public GameObject recipeBook;
    public KeyCode OpenBook = KeyCode.Alpha2;
    public CameraMovement movCam;
    public Canvas disabledCanvas;
    public RayCast_Test rayCast_testScript;

    void Update()
    {
        if (Input.GetKeyDown(OpenBook))
        {
            if (!recipeBook.activeSelf)
            {
                recipeBook.SetActive(true);
                movCam.enabled = false;
                disabledCanvas.enabled = false;
                UnityEngine.Cursor.lockState = CursorLockMode.Confined;
                rayCast_testScript.enabled = false;
            }
            else
            {
                recipeBook.SetActive(false);
                movCam.enabled = true;
                disabledCanvas.enabled = true;
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
				rayCast_testScript.enabled = true;
			}
        }
    }
}
