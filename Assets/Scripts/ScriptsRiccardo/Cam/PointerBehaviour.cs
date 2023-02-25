using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PointerBehaviour : MonoBehaviour
{
    // private
    [SerializeField] private PickUpDrop_Simple pickUpDrop_Script;
    [SerializeField] private Color pointerColorGrab;
    [SerializeField, Range(1f, 5f)] private float pointerIncreaseSize;
    private Renderer pointerRenderer;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible= false;
        pointerRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (pickUpDrop_Script.pickable)
        {
            // change pointer color & scale
            if (pointerRenderer.sharedMaterial.color == Color.white)
            {
                transform.localScale += new Vector3(pointerIncreaseSize / 1000, pointerIncreaseSize / 1000, 0f);
                pointerRenderer.material.color = pointerColorGrab;
            }
        }
        else
        {
            // change pointer color & scale
            if (!pickUpDrop_Script.grab && pointerRenderer.sharedMaterial.color == pointerColorGrab)
            {
                transform.localScale -= new Vector3(pointerIncreaseSize / 1000, pointerIncreaseSize / 1000, 0f);
                pointerRenderer.material.color = Color.white;
            }
        }       
    }
}
