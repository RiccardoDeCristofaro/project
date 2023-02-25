using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RayCast_Test : MonoBehaviour
{
    public Transform ObjectContainer;
    [SerializeField, Range(1f, 10f)] private float pickUpRange;

    internal Pointer pointer_Script;
    [SerializeField] internal KeyCode pickUpButton;
    [SerializeField] internal KeyCode dropButton;
    internal LayerMask permittedLayer;
    internal GameObject pickObject;
    internal RaycastHit stationObject;
    internal GameObject objectRaycast;
    internal bool raycastHit = false;
    internal bool positioning;

    internal Ingredient_Info ingredient_Info;

    private void Start()
    {
        permittedLayer = LayerMask.GetMask("LayerPick") | LayerMask.GetMask("LayerReady");
    }

    void Update()
    {
        // raycasting: check if the raycast is colliding with an object on the permitted layer's
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, pickUpRange, permittedLayer))
        {
            // save object hitted by raycast
            objectRaycast = hit.transform.gameObject;

            if (objectRaycast.layer == LayerMask.NameToLayer("LayerPick"))
                pickObject = hit.transform.gameObject;
            else if (objectRaycast.layer == LayerMask.NameToLayer("LayerInteractable") || objectRaycast.layer == LayerMask.NameToLayer("LayerReady"))
            {
                stationObject = hit;
                if (objectRaycast.layer == LayerMask.NameToLayer("LayerInteractable"))
                    positioning = ValidateManager(true);                          
            }
            else if (objectRaycast.layer != LayerMask.NameToLayer("LayerInteractable"))
                positioning = false;
             
            MouseClick(hit);

            if (!raycastHit)
                raycastHit = true;
            Debug.DrawRay(transform.position, transform.forward * pickUpRange, Color.green);
        }
        else
        {
            if (raycastHit)
                raycastHit = false;

            Debug.DrawRay(transform.position, transform.forward * pickUpRange, Color.red);

        }
    }

    private void MouseClick(RaycastHit hit)
    {
        if (Input.GetKeyDown(pickUpButton))
        {
            // grab object
            if (objectRaycast.layer == LayerMask.NameToLayer("LayerPick"))
                SimplePickUp(pickObject.transform.gameObject);

            // interact
            else if (objectRaycast.layer == LayerMask.NameToLayer("LayerInteractable") && positioning)
                ValidateManager(false);

            else if (objectRaycast.layer == LayerMask.NameToLayer("LayerReady"))
            {
                // spawn object
                if (objectRaycast.tag == "SpawnerBox")
                {
                    SpawnerBox SpawnerBox_Script;
                    SpawnerBox_Script = objectRaycast.GetComponent<SpawnerBox>();                  
                    SpawnerBox_Script.enable = true;
                }
                  
                else if (objectRaycast.tag == "CookBook")
                {
                    // open cookBook
                }
            }
        }
    }
    private bool ValidateManager(bool validate)
    {
        if (ObjectContainer.GetChild(0).tag == "Ladle")
            ingredient_Info = ObjectContainer.GetComponentInChildren<Ingredient_Info>();
        else
            ingredient_Info = pickObject.transform.gameObject.GetComponent<Ingredient_Info>();

        string station_tag = stationObject.transform.tag;
        if (station_tag == "CuttingBoard")
        {
            CuttingBoard cuttingBoard_Script;
            cuttingBoard_Script = stationObject.transform.GetComponent<CuttingBoard>();

            if (validate)
                return cuttingBoard_Script.Validate(ingredient_Info, out cuttingBoard_Script.ingredient);
            else
            {
                SimpleDrop(pickObject.transform.gameObject);
                Positioning(pickObject.transform.gameObject, cuttingBoard_Script.spawnPoint, true);
                cuttingBoard_Script.enable = true;
                stationObject.transform.gameObject.layer = LayerMask.NameToLayer("LayerReady");
                return false;
            }
        }
        else if (station_tag == "Grill")
        {
            Grill grill_Script;
            grill_Script = stationObject.transform.GetComponent<Grill>();

            if (validate)
                return grill_Script.Validate(ingredient_Info, out grill_Script.ingredient);
            else
            {
                SimpleDrop(pickObject.transform.gameObject);
                Positioning(pickObject.transform.gameObject, grill_Script.spawnPoint, true);
                grill_Script.enable = true;
                stationObject.transform.gameObject.layer = LayerMask.NameToLayer("LayerReady");
                return false; ;
            }
        }
        else if (station_tag == "Plate" || station_tag == "Pot")
        {
            Plate plate_Script;
            plate_Script = stationObject.transform.GetComponent<Plate>();

            if (validate)
                return plate_Script.Validate(ingredient_Info, out plate_Script.ingredient);
            else
            {
                SimpleDrop(pickObject.transform.gameObject);
                plate_Script.enable = true;
                return false;
            }
        }
        else if (station_tag == "Bowl")
        {
            Bowl bowl_Script;
            bowl_Script = stationObject.transform.GetComponent<Bowl>();

            if (validate)
            {
                return bowl_Script.Validate(pickObject.transform.gameObject, out bowl_Script.recipe);
            }
            else
            {
                SimpleDrop(pickObject.transform.gameObject);
                bowl_Script.enable = true;
                return false;
            }
        }
        else if (station_tag == "Cannon" && ObjectContainer.GetChild(0).tag != "Ladle")
        {
            Cannon cannon_Script;
            cannon_Script = stationObject.transform.GetComponent<Cannon>();

            if (validate)
                return cannon_Script.Validate(pickObject.transform.gameObject);
            else
            {
                SimpleDrop(pickObject.transform.gameObject);
                Positioning(pickObject.transform.gameObject, cannon_Script.fireLauncher, true);
                stationObject.transform.gameObject.layer = LayerMask.NameToLayer("LayerReady");
                return false;
            }
        }
        else if (station_tag == "Table")
        {
            if (validate)
                return true;
            else
            {
                SimpleDrop(pickObject.transform.gameObject);
                Positioning(pickObject.transform.gameObject, stationObject.transform.GetChild(0).transform, true);
                return false;
            }
        }
        else if (station_tag == "Trash")
        {
            if (validate)
                return true;
            {
                SimpleDrop(pickObject.transform.gameObject);
                Destroy(pickObject.transform.gameObject);
                return false;
            }
        }
        return true;
    }
    public void SimplePickUp(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        Collider col = obj.GetComponent<Collider>();

        rb.constraints = RigidbodyConstraints.None;

        // if the pickObject has a parent
        if (obj.transform.parent != null)
        {
            // activate Spawner box
            if (obj.transform.root.tag != "SpawnerBox")
                obj.transform.root.gameObject.layer = LayerMask.NameToLayer("LayerInteractable");

            obj.transform.SetParent(null);          
        }

        // change mask
        permittedLayer = LayerMask.GetMask("LayerInteractable");
        obj.transform.SetParent(ObjectContainer);
        obj.transform.localPosition = Vector3.zero;
        rb.isKinematic = true;
        col.isTrigger = true;      
    }
    private void SimpleDrop(GameObject obj)
    {

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        Collider col = obj.GetComponent<Collider>();

        // change mask
        permittedLayer = LayerMask.GetMask("LayerPick") | LayerMask.GetMask("LayerReady");
        obj.transform.SetParent(null);

        if (stationObject.transform.tag != "Cannon")
        {
            col.isTrigger = false;
            rb.isKinematic = false;
        }      
    }
    public Ingredient_Info ProcessCreation(GameObject toCreate, GameObject toDestory, Transform spawnPoint, bool parent)
    {
 
        GameObject result;
        Ingredient_Info toDestory_Info = toDestory.transform.gameObject.GetComponent<Ingredient_Info>();
       
        result = Instantiate(toCreate, spawnPoint.position, Quaternion.identity);
 
        if (pickObject.tag == "Ladle")
        {
            Debug.Log("11");
            Recipe_Info result_Info = result.transform.gameObject.GetComponent<Recipe_Info>();
            Debug.Log("22");
            result_Info.quality = toDestory_Info.quality;
            Debug.Log("33");
        }
        else
        {
            if (result.GetComponent<Ingredient_Info>() != null)
            {             
                Ingredient_Info result_Info = result.transform.gameObject.GetComponent<Ingredient_Info>();
     
                result_Info.quality = toDestory_Info.quality;
                Positioning(result, spawnPoint, parent);
 
                Destroy(toDestory);
                return result_Info;
            }
            else if (result.GetComponent<Recipe_Info>() != null)
            {
                Recipe_Info result_Info = result.transform.gameObject.GetComponent<Recipe_Info>();
                Plate plate_Script;
                plate_Script = stationObject.transform.GetComponent<Plate>();
                result_Info.quality = plate_Script.QualityCalculation();
                Positioning(result, spawnPoint, parent);
                Destroy(toDestory);
            }
        }
        return null;
    }
    public void Spawn(GameObject toCreate, Transform spawnPoint)
    {
        GameObject result;
        result = Instantiate(toCreate, spawnPoint.position, Quaternion.identity);
        Positioning(result, spawnPoint, true);
    }
    public void Positioning(GameObject toPlace, Transform spawnPoint, bool parent)
    {
        Debug.Log("4");
        if (parent)
        {
            toPlace.transform.SetParent(spawnPoint);
            toPlace.transform.localPosition = Vector3.zero;
        }
        toPlace.transform.rotation = Quaternion.Euler(0, 0, 0);
        Debug.Log("5");
    }  
    public void SwitchLayer(Transform parent, string fromLayer, string toLayer)
    {
        int rootlayerValue = parent.transform.parent.gameObject.layer;

        if (parent.childCount == 1 && rootlayerValue == LayerMask.NameToLayer(fromLayer))
            parent.transform.parent.gameObject.layer = LayerMask.NameToLayer(toLayer);
        if (parent.childCount == 0 && rootlayerValue == LayerMask.NameToLayer(toLayer))
            parent.transform.parent.gameObject.layer = LayerMask.NameToLayer(fromLayer);
    }

    public bool Interact(string tag)
    {
        if (objectRaycast !=  null)
        {
            if (Input.GetKey(KeyCode.Mouse0) && raycastHit && objectRaycast.transform.tag == tag)
                return true;         
        }
        return false;
    }
    public bool NewInteract(string name)
    {
        if (objectRaycast != null)
        {
            if (Input.GetKey(KeyCode.Mouse0) && raycastHit && objectRaycast.transform.name == name)
                return true;
        }
        return false;
    }
}
