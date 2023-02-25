using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Pointer: MonoBehaviour
{
    [SerializeField] private RayCast_Test rayCast_Test;
    [SerializeField] private Color color_Default;
    [SerializeField] private Color color_Grab;
    public Color color_Interact;
    [SerializeField] private Color color_Ready;
    [SerializeField, Range(1f, 20f)] private float increaseSize;
    private Renderer pointerRenderer;
    private Vector3 scale_Default;

    // layer = pick -> green
    // layer = ready -> red
    // positioning = true -> blue
    void Start()
    {
        pointerRenderer = GetComponent<Renderer>();
        pointerRenderer.material.color = color_Default;
        scale_Default = pointerRenderer.transform.localScale;
    }

    void Update()
    {
        if (rayCast_Test.raycastHit)
            PointerColor(LayerMask.LayerToName(rayCast_Test.objectRaycast.layer));
        else
            pointerRenderer.material.color = color_Default;

        // set default pointer scale & color
        if (pointerRenderer.sharedMaterial.color == color_Default && transform.localScale != scale_Default)
            transform.localScale -= new Vector3(increaseSize / 1000, increaseSize / 1000, 0f);           
    }

    private void PointerColor(string layerName)
    {
        // collect the color relative to the objectHit layer name (color adjust)
        // objectHit: the object hitted by rayCast.
        Color pointerColorAdjust = ColorAdjustment();

        // if the color should change, change it whit color adjust
        if (pointerRenderer.sharedMaterial.color != pointerColorAdjust)
            pointerRenderer.material.color = pointerColorAdjust;

        // change pointer scale
        if (pointerRenderer.sharedMaterial.color != color_Default && transform.localScale == scale_Default)
            transform.localScale += new Vector3(increaseSize / 1000, increaseSize / 1000, 0f);             
    }

    private Color ColorAdjustment()
    {
        if (!Input.GetKey(KeyCode.Mouse0) && rayCast_Test.objectRaycast.layer == LayerMask.NameToLayer("LayerPick"))
            return color_Grab;
        else if (rayCast_Test.objectRaycast.layer == LayerMask.NameToLayer("LayerReady"))
            return color_Ready;
        else if (!Input.GetKey(KeyCode.Mouse0) && rayCast_Test.positioning)
            return color_Interact;
      
        return color_Default;
    }
}
