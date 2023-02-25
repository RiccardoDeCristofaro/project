using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform fireLauncher;
    public RayCast_Test rayCast_Test;
    public ChangeCam changeCam_Script;

    private bool startCannon;

    private void Start()
    {
        startCannon = false;
    }

    private void Update()
    {
        if (startCannon && gameObject.layer == LayerMask.NameToLayer("LayerReady"))
        {
            if (rayCast_Test.Interact(gameObject.tag))
            {
                Collider col = rayCast_Test.pickObject.GetComponent<Collider>();
                Physics.IgnoreCollision(col, rayCast_Test.stationObject.collider);
                changeCam_Script.EnterCannon();
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && !startCannon)
            startCannon = true; // you can switch     
    }

    public bool Validate(GameObject toValidate)
    {
        if (toValidate.tag == "Recipe")
        {
            Debug.Log("You can insert recipe");
            return true;
        }

        Debug.Log("You can't insert recipe");
        return false;
    }
}
