using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Recipe_Info : MonoBehaviour
{
    [SerializeField] internal float quality;
    public Nationality nationality;
    public List<Ingredient_Info> ingredients = new List<Ingredient_Info>();
    [SerializeField] internal float boilTime;
    [SerializeField] internal float boilBurnTime;
}
public enum Nationality
{
    English,
    Spanish,
    Italian
}

