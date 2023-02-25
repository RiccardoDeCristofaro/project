using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class BoatPoints : MonoBehaviour
{
	[SerializeField] public Text point_Txt;
    [SerializeField] public int currentScore = 500;
    public ShakeCam ShakeCam;
    public SliderHP slider_Hp_Script;

    private void Start()
    {
        point_Txt.text = currentScore.ToString();
    }
    public void SuccessCheck()
    {
        currentScore += 60;
        point_Txt.text = currentScore.ToString();
        
    }
    public void BuyItem()
    {
        currentScore -= 10;
        point_Txt.text = currentScore.ToString();
    }

    public void ShipwrongHit()
    {
        point_Txt.text = currentScore.ToString();
        StartCoroutine(ShakeCam.Shake());
        //slider_Hp_Script.HealthPoint();
    }
}
