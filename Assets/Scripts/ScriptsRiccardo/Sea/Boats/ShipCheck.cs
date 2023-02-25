using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShipCheck : MonoBehaviour
{
	public Nationality shipNationality = Nationality.Spanish;
	//public string typeFoodShip;

	public BoatPoints points_Text;
	public ShakeCam shakecam;

	private void OnTriggerEnter(Collider recipeThrown)// other == food delivery
	{


		Recipe_Info recipe = recipeThrown.gameObject.GetComponent<Recipe_Info>();

		if (recipe.nationality == shipNationality && recipe.quality >= 2)
		{
			// points
			Debug.Log("hit Ship and correct nationality");
			points_Text.SuccessCheck();
		}
		else if (recipe.nationality == shipNationality && recipe.quality < 2)
		{
			Debug.Log("hit ship , but not perfect recipe, it's burnt");
			points_Text.ShipwrongHit();
			StartCoroutine(shakecam.Shake());
		}
		else if (recipe.nationality != shipNationality)
		{
			Debug.Log("No boat hit");
			points_Text.ToString();
			points_Text.ShipwrongHit();
			StartCoroutine(shakecam.Shake());
		}
		Destroy(recipeThrown.gameObject);

	}
}
