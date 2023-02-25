using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class PickUpDrop_Simple : MonoBehaviour
{
    public Transform ObjectContainer;

    public KeyCode pickUpButton;
    public KeyCode dropButton;
    public KeyCode throwButton;

    public LayerMask pickUpLayer;
    [SerializeField, Range(1f, 6f)] private float pickUpRange;

    public RaycastHit hitObject;
    public string objectGrabName;

    internal bool grab = false;
    internal bool pickable = false;

    public float ThrowForce = 8f;

    void Update()
    {
        if (!grab)
        {
            // raycasting: check if the object colliding with raycast is on layer Objects
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, pickUpRange, pickUpLayer))
            {

                
                // pickup object
                if (Input.GetKeyDown(pickUpButton))
                {
                    // collect object infos
                    Debug.Log(hit.transform.gameObject.name);
                    objectGrabName = hit.transform.gameObject.name;
                    hitObject = hit;      
                    SimplePickUp(hitObject);
                }
                if (!pickable)
                    pickable = true;
                Debug.DrawRay(transform.position, transform.forward * pickUpRange, Color.green);
            }
            else
            {
                if (pickable)
                    pickable = false;

                Debug.DrawRay(transform.position, transform.forward * pickUpRange, Color.red);
            }
        }
        else
        {
            // drop
            if (Input.GetKeyDown(dropButton))
                SimpleDrop(hitObject);
            // throw
            if (Input.GetKeyDown(throwButton))
                SimpleThrow(hitObject);
            // pose
            
        }
    }
    private void SimplePickUp(RaycastHit hitInfo)
    {
        grab = true;
        hitInfo.transform.SetParent(ObjectContainer);
        hitInfo.transform.localPosition = Vector3.zero;
        hitInfo.rigidbody.isKinematic = true;
        hitInfo.collider.isTrigger = true;
        
    }
    internal void SimpleDrop(RaycastHit hitInfo)
    {
        grab = false;
        hitInfo.transform.SetParent(null);
        hitInfo.rigidbody.isKinematic = false;
        hitInfo.collider.isTrigger = false;
    }

    private void SimpleThrow(RaycastHit hitInfo)
    {
        grab = false;
        hitInfo.transform.SetParent(null);    
        hitInfo.rigidbody.isKinematic = false;
        hitInfo.collider.isTrigger = false;
        hitObject.rigidbody.AddForce(transform.forward * ThrowForce, ForceMode.Impulse); 
    }

    
}
