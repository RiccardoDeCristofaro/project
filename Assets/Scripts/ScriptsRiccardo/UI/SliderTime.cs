using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTime : MonoBehaviour
{

	// time manager
	public Slider timeSlide;
	[SerializeField] private float speedTime = 1f;
	public Text TimeText;
	private void Update()
	{

		Timer();
	}
	public void Timer()
	{
		timeSlide.value -= Time.deltaTime * speedTime;
		TimeText.text = Mathf.Round(timeSlide.value).ToString();
	}
}
