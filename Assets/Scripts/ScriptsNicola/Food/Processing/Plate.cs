using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public Recipe_List list;
    public RayCast_Test rayCast_Test;
    public Pot pot_Script;

    [SerializeField] internal GameObject objectLinked;

    private List<Ingredient_Info> validateIngredients = new List<Ingredient_Info>();
    private List<Ingredient_Info> ingredients_Clone = new List<Ingredient_Info>();

    internal GameObject recipe_Object;
    internal Recipe_Info recipe_Info;

    internal Ingredient_Info ingredient;
    internal bool enable;

    [SerializeField] internal int recipeNumber;
    private void Start()
    {
        AssigneRecipe(list.recipes[recipeNumber]);
    }
    private void Update()
    {
        if (enable)
        {
            validateIngredients.Add(ingredient);
            ingredients_Clone.Remove(ingredient);

            Destroy(rayCast_Test.pickObject.transform.gameObject);

            // create complete dish when all ingredients have been validated
            if (ingredients_Clone.Count == 0f)
            {
                switch (gameObject.tag)
                {
                    case "Plate":
                        rayCast_Test.ProcessCreation(recipe_Object, gameObject, objectLinked.transform, false);
                        break;
                    case "Pot":
                        {
                            pot_Script.enable = true;
                            objectLinked.SetActive(true);
                            gameObject.layer = LayerMask.NameToLayer("LayerReady");
                            pot_Script.qualityValue = recipe_Info.quality;
                            pot_Script.recipe = recipe_Info;
                            break;
                        }                                             
                }                                      
            }
               
            enable = false;
        }
    }
    public float QualityCalculation()
    {
        Debug.Log("Quality calculation");
        return 0f;
    }
    public bool Validate(Ingredient_Info ingredient, out Ingredient_Info ingredientFound)
    {
        ingredientFound = null;
        if (ingredient != null)
        {
            // find ingredient in ingredients 
            foreach (Ingredient_Info i in ingredients_Clone)
            {
                if (ingredient.id == i.id)
                {
                    ingredientFound = i;
                    Debug.Log("The ingredient is correct");
                    return true;
                }
            }
            // find ingredient in validateIngredients 
            foreach (Ingredient_Info i in validateIngredients)
            {
                if (ingredient.id == i.id)
                {
                    Debug.Log("You have already insert this ingredient");
                    return false;
                }
            }
            Debug.Log("The ingredient is incorrect");     
        }
        return false;

    }

    public void AssigneRecipe(GameObject clicked_Recipe)
    {
        recipe_Object = clicked_Recipe;

        ingredients_Clone.Clear();
        validateIngredients.Clear();

        recipe_Info = recipe_Object.GetComponent<Recipe_Info>();

        foreach (Ingredient_Info ingredient in recipe_Info.ingredients)
            ingredients_Clone.Add(ingredient);
    }
}
