using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Recipe_List : MonoBehaviour
{
    public List<GameObject> recipes = new List<GameObject>();
    /*
     
    //Auto populate recipes
     
    public void Awake()
    {
        Recipe_ListCreation();
    }

    
    public void Recipe_ListCreation()
    {
        string[] assetNames = AssetDatabase.FindAssets("", new string[] { "Assets/Prefabs/Recipes" });

        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var recipe = AssetDatabase.LoadAssetAtPath<GameObject>(SOpath);
            recipes.Add(recipe);
        }
    }
    */
}
