using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    public RayCast_Test rayCast_Test;

    [SerializeField] private Transform spawnPoint;

    internal GameObject recipe;
    internal bool enable;

    private void Update()
    {
        if (enable)
        {           
            rayCast_Test.ProcessCreation(rayCast_Test.pickObject.GetComponent<Ingredient_Info>().cutResult, rayCast_Test.pickObject, spawnPoint, false);
            Destroy(rayCast_Test.pickObject);
            rayCast_Test.permittedLayer = LayerMask.GetMask("LayerPick") | LayerMask.GetMask("LayerReady");
            Debug.Log("Plating soup");
            Destroy(gameObject);
        }
           
    }
    public bool Validate(GameObject toValidate, out GameObject recipe)
    {
        recipe = null;
        if (toValidate.tag == "Ladle")
        {
            Debug.Log("The ingredient is correct");
            return true;
        }
        else
            Debug.Log("The ingredient is incorrect");      
        return false;
    }
}
