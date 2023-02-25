using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    public GameObject selectedObject;
    public Material selectedMaterial;
  

    private void Start()  { }

    private void Update()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition); // select whatever the camera is looking

        RaycastHit hit;

        if (Physics.Raycast(cameraRay, out hit,5f))
        {
            if(hit.transform.tag == "PickableObj" || hit.transform.tag == "Cannon")
            {
                GameObject hitObject = hit.transform.root.gameObject; // work properly for no nested objects;              
                Select(hitObject);
            }
            
        }
        else
            ClearSelection();

    }

    void Select(GameObject objectToSelect)
    {
        if (selectedObject != null)
        {
            if (objectToSelect == selectedObject)
                return;

            ClearSelection();
        }

        selectedObject = objectToSelect;


        Renderer[] selectObjRender = selectedObject.GetComponentsInChildren<Renderer>(); // get the component

        foreach (Renderer renderer in selectObjRender)
        {
            renderer.material.EnableKeyword("_EMISSION");
        }
    }
    void ClearSelection()
    {
        if(selectedObject == null)
            return ;

        Renderer[] UnselectObjRender = selectedObject.GetComponentsInChildren<Renderer>(); 
        foreach (Renderer renderer in UnselectObjRender)
        {
            renderer.material.DisableKeyword("_EMISSION");
        }

        selectedObject = null;
    }
}
