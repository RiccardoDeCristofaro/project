using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public RayCast_Test rayCast_Test;
    public Plate plate_Script;
    [SerializeField] internal GameObject ladle;
    private bool endBoil;
    internal bool enable;
    [SerializeField] private float timeMix;
    internal Recipe_Info recipe;

    internal float qualityValue;
    private float timer_Boil;
    private float timer_Burn;
    private float timer_Mix;
    private bool startBoil;
    private bool boil;
    private void Start()
    {
        timer_Boil = 0f;
        timer_Burn = 0f;
        timer_Mix = 0f;
        startBoil = false;
        boil = false;
       
    }
    private void Update()
    {
        if (endBoil && rayCast_Test.NewInteract(gameObject.name))
        {
            Ingredient_Info ladle_info;
            GameObject result = Instantiate(ladle, rayCast_Test.ObjectContainer.transform.position, Quaternion.identity);
            rayCast_Test.pickObject = result;
            ladle_info = result.GetComponent<Ingredient_Info>();
            ladle_info.quality = (int)qualityValue;
            ladle_info.cutResult = plate_Script.recipe_Object;
            plate_Script.objectLinked.SetActive(false);
            gameObject.layer = LayerMask.NameToLayer("LayerInteractable");
            rayCast_Test.permittedLayer = LayerMask.GetMask("LayerInteractable");
            rayCast_Test.SimplePickUp(result);
            endBoil = false;
            ResetParameters();
        }

        if (enable)
        {          
            if (Input.GetKeyUp(KeyCode.Mouse0) && !startBoil)
            {
                Debug.Log("Start");
                startBoil = true; // you can grill     
                boil = true;
            }

            if (startBoil)
                BoilProcess();
        }
    }
    private void BoilProcess()
    {
        // burn
        if (qualityValue > 0f)
        {
            // grill
            if (boil)
            {
                if (timer_Boil < recipe.boilTime) //--- timer grill
                    timer_Boil += Time.deltaTime;

                //creation
                else
                {
                    timer_Boil = 0f;
                    boil = false;
                    endBoil = true;
                    Debug.Log("AAAAAAAAAAAAAAAAAAAAAn" + qualityValue);
                    Debug.Log("Normal Reset");
                }
            }

            // flip
            if (rayCast_Test.NewInteract(gameObject.name))
            {
                if (timer_Mix < timeMix)              //--- timer flip
                    timer_Mix += Time.deltaTime;
                else
                {
                    timer_Mix = 0f;
                    timer_Burn = 0f;
                }
            }
            else
                timer_Mix = 0f;

            if (timer_Burn < recipe.boilBurnTime) //--- timer burn
                timer_Burn += Time.deltaTime;

            else
            {
                qualityValue--;
                Debug.Log("fffffffffffffffff" + qualityValue);
                Debug.Log(qualityValue);
                timer_Burn = 0f;
            }
        }
        // end + creation
        else
        {
            if (boil)
            {
                endBoil = true;
                Debug.Log("SSSSSSSSSSSSSSSSSSSSS" + qualityValue);
            }

            Debug.Log("Burn reset");
            ResetParameters();
        }
    }
    public void ResetParameters()
    {
        timer_Burn = 0f;
        timer_Mix = 0f;
        timer_Boil = 0f;
        boil = false;
        enable = false;
        startBoil = false;
        Debug.Log("Reset");
    }
}
