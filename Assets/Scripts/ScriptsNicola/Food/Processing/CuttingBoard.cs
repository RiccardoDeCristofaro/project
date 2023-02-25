using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : MonoBehaviour
{
    public Transform spawnPoint;
    public RayCast_Test rayCast_Test;

    internal Ingredient_Info ingredient;
    internal bool enable;

    private float timer_Cut;
    private bool startCut;
	public GameObject animationKnife;
	private Animator animatorKnife;

	private void Start()
    {
        timer_Cut = 0f;
        startCut = false;
        enable = false;
    }

    void Update()
    {      
        // cuttingBoard logic on
        if (enable)
        {
            if (spawnPoint.childCount == 0)
                ResetParameters();

            if (Input.GetKeyUp(KeyCode.Mouse0) && !startCut)
                startCut = true; // you can cut     

            if (startCut)
                Cut_Process();

            rayCast_Test.SwitchLayer(spawnPoint, "LayerInteractable", "LayerReady");
        }
    }

    private void Cut_Process()
    {
        if (rayCast_Test.NewInteract(gameObject.name))
        {
            // cut
            if (timer_Cut < ingredient.cuttingTime)
            {
                timer_Cut += Time.deltaTime;            // --- timer cut          
                spawnPoint.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");
                Debug.Log("CuttingProcess...");
				animationKnife.SetActive(true);
				animatorKnife = animationKnife.GetComponent<Animator>();
				animatorKnife.Play("KnifeAnimation");
			}

            // end + creation
            else
            {            
                spawnPoint.GetChild(0).gameObject.layer = LayerMask.NameToLayer("LayerPick");
                gameObject.layer = LayerMask.NameToLayer("Default");
                ingredient = rayCast_Test.ProcessCreation(ingredient.cutResult, ingredient.gameObject, spawnPoint, true);
                ResetParameters();
            }
        }
        else
        {
            // reset
            if (timer_Cut > 0f)
            {
                spawnPoint.GetChild(0).gameObject.layer = LayerMask.NameToLayer("LayerPick");
                timer_Cut = 0f;
				animationKnife.SetActive(false);
				Debug.Log("Reset");
            }
        }
    }

    public bool Validate(Ingredient_Info ingredient, out Ingredient_Info ingredient_Info)
    {
        ingredient_Info = null;

        if (ingredient.cutResult != null)
        {
            ingredient_Info = ingredient;
            Debug.Log("You can cut");
            return true;
        }
        else
        {
            Debug.Log("You can't cut");
            return false;
        }
    }

    private void ResetParameters()
    {
        timer_Cut = 0f;
        enable = false;
        startCut = false;
		animationKnife.SetActive(false);
	}
}

