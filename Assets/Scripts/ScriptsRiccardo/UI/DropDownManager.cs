using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public static class ButtonExtension
{
    public static void AddEventListenernNew<T> (this Button target, T param, Action<T> OnClick) // get the type of the action whose is connected wiht the button;
    {
        target.onClick.AddListener(delegate ()
        {
            OnClick(param);
        });
    }
}
public class DropDownManager : MonoBehaviour
{
    [SerializeField] private GameObject[] recipe_info; // list of recipes
    [SerializeField] private Plate plate;
    private GameObject gmObj;
    public void ClickedRecipe()
    {
        Recipe_Info savedRecipe;
        GameObject button = transform.GetChild(0).gameObject;
        
        int N = recipe_info.Length;

        for (int i = 0; i < N; i++)
        {
            
            savedRecipe = recipe_info[i].GetComponent<Recipe_Info>();    
            // set values
            gmObj.transform.GetChild(0).GetComponent<Text>().text = savedRecipe.nationality.ToString();
            gmObj.transform.GetChild(1).GetComponent<Text>().text = savedRecipe.ingredients.ToString();
            gmObj.GetComponent<Button>().AddEventListenernNew(i, ItemClicked);
            plate.AssigneRecipe(recipe_info[i]);
            
        }
    }

    private void ItemClicked(int itemIndex)
    {
        // print
        Debug.Log("item: " + itemIndex + "clicked");

        Debug.Log("name: " + recipe_info[itemIndex].ToString());


    }
}
