using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingObjects : MonoBehaviour
{
    public static string targetObj;
    public string internalObj;
    public RaycastHit theobject;
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out theobject)) 
        {
            targetObj = theobject.transform.gameObject.name;
            internalObj = theobject.transform.gameObject.name;
        }
    }
}
