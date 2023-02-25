using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class PickUpDrop : MonoBehaviour
{
    public Transform ObjectContainer;
    public KeyCode pickUpButton;
    public KeyCode dropButton;
    public LayerMask pickUpLayer;
    [SerializeField, Range(1f, 10f)] private float pickUpRange;
    internal RaycastHit hitObject;
    internal bool grab = false;
    internal bool raycastHit = false;

    void Update()
    {
        if (!grab)
        {
            if (pickUpLayer == LayerMask.GetMask("LayerInteractable"))
            {
                pickUpLayer = LayerMask.GetMask("LayerPick");
            }

            // raycasting: check if the object colliding with raycast is on layer Objects
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, pickUpRange, pickUpLayer))
            {
                // collect object infos
                hitObject = hit;

                if (!raycastHit)
                    raycastHit = true;

                // pickup object
                if (Input.GetKeyDown(pickUpButton))
                {                  
                    Debug.Log(hitObject.transform.gameObject.name);
                    SimplePickUp(hitObject);
                }
                Debug.DrawRay(transform.position, transform.forward * pickUpRange, Color.green);

            }
            else
            {
                if (raycastHit)
                    raycastHit = false;
                Debug.DrawRay(transform.position, transform.forward * pickUpRange, Color.red);
            }
        }

        else
        {
            if (pickUpLayer == LayerMask.GetMask("LayerPick"))
            {
                pickUpLayer = LayerMask.GetMask("LayerInteractable");
            }

            // drop
            if (Input.GetKeyDown(dropButton))
                SimpleDrop(hitObject);

            Debug.DrawRay(transform.position, transform.forward * pickUpRange, Color.yellow);
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
    private void SimpleDrop(RaycastHit hitInfo)
    {
        grab = false;
        hitInfo.transform.SetParent(null);
        hitInfo.rigidbody.isKinematic = false;
        hitInfo.collider.isTrigger = false;
    }
}
