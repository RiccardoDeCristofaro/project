using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : MonoBehaviour
{
    public Transform spawnPoint;
    public RayCast_Test rayCast_Test;
    [SerializeField] private float timeFlip;

    internal Ingredient_Info ingredient;
    internal bool enable;

    private float timer_Grill;
    private float timer_Burn;
    private float timerFlipClick;
    private bool startGrill;
    private bool grill;

    // grill animation 
    private Animator animatorSpatula;
    private Animator animateFlip;

    internal Ingredient_Info cloned_Object;
    public GameObject spatula;
    public GameObject flippingObject;

    public Transform flippingPoint;

    private bool isAnimating;
    private void Start()
    {
        timer_Grill = 0f;
        timer_Burn = 0f;
        timerFlipClick = 0f;
        //startGrill = true;
        grill = false;
        isAnimating = false;
        animatorSpatula = GetComponent<Animator>();
        animateFlip = flippingObject.GetComponent<Animator>();
    
}

    void Update()
    {

        // grill logic on
        if (enable)
        {
            if (spawnPoint.childCount == 0)
                ResetParameters();

            if (Input.GetKeyUp(KeyCode.Mouse0) && !startGrill)
            {
                startGrill = true; // you can grill     
                grill = true;
            }

            if (startGrill)
            {
                Grill_Process();
            }

            rayCast_Test.SwitchLayer(spawnPoint.transform, "LayerInteractable", "LayerReady");
        }
    }

    private void Grill_Process()
    {
        // burn
        if (ingredient.quality > 0f)
        {

            // grill
            if (grill)
            {
                if (timer_Grill < ingredient.grillTime)
                {
                    timer_Grill += Time.deltaTime;  //--- timer grill
                }
                else //creation
                {
                    timer_Grill = 0f;

                    ingredient = rayCast_Test.ProcessCreation(ingredient.grillResult, ingredient.gameObject, spawnPoint, true);
                    grill = false;
                    
                    Debug.Log("Normal Creation");
                    Debug.Log("Normal Reset");
                }
            } // NO ANIMATION

            // flip
            if (rayCast_Test.NewInteract(gameObject.name)) // interagire stazione con nome preciso 
            {

                if (timerFlipClick < timeFlip) //--- timer flip
                {
                    timerFlipClick += Time.deltaTime;
                }
                else
                {
                    // play animation
                    AnimateFlipPlay();
                    timer_Burn = 0f;
                    timerFlipClick = 0f;

                }

                // not interact

                if (timer_Burn < ingredient.grillBurnTime)//--- timer burn
                {
                    timer_Burn += Time.deltaTime;
                }
                else
                {
                    ingredient.quality--;
                    timer_Burn = 0f;
                }

            }
            // end + creation
            else
            {
                #region burnBegin
                if (grill)
                {
                    ingredient = rayCast_Test.ProcessCreation(ingredient.grillResult, ingredient.gameObject, spawnPoint, true);
                    timer_Burn = 0f;
                    timerFlipClick = 0f;
                    timer_Grill = 0f;
                   
                    Debug.Log("Burn creation");
                }

                Debug.Log("Burn reset");
                ResetParameters();
                #endregion
            }
        }
    }
    private void ActivateObject() // unire animate flip
    {
        isAnimating = true;
        
        if (cloned_Object == null)
        {
            cloned_Object = Instantiate(ingredient, flippingPoint);
            cloned_Object.GetComponent<Rigidbody>().isKinematic = true;
            cloned_Object.GetComponent<BoxCollider>().enabled = false;
        }
        //ingredient.gameObject.SetActive(false);
        ingredient.GetComponent<MeshRenderer>().enabled = false;
        ingredient.GetComponent<Rigidbody>().isKinematic = true;
        ingredient.GetComponent<BoxCollider>().enabled = false;
        spatula.SetActive(true);
        flippingObject.SetActive(true);
    }
    private void AnimateFlipPlay()
    {
        // interupt all the process , the timers and actions
        ActivateObject();
        animateFlip.Play("ObjectFlipping");
        animatorSpatula.Play("SpatulaAnimation");
        DisactiveObject();
    }
    private void DisactiveObject()
    {
        Destroy(cloned_Object.gameObject);
        ingredient.gameObject.SetActive(true);
        ingredient.GetComponent<Rigidbody>().isKinematic = false;
        ingredient.GetComponent<BoxCollider>().enabled = true;
        isAnimating = false;
    }
    public bool Validate(Ingredient_Info ingredient, out Ingredient_Info ingredient_Info)
    {
        ingredient_Info = null;

        if (ingredient.grillResult != null)
        {
            ingredient_Info = ingredient;
            Debug.Log("You can grill");
            return true;
        }
        else
        {
            Debug.Log("You can't grill");
            return false;
        }
    }
    private void ResetParameters()
    {
        timer_Burn = 0f;
        timerFlipClick = 0f;
        timer_Grill = 0f;
        grill = false;
        enable = false;
        startGrill = false;
    }

}
