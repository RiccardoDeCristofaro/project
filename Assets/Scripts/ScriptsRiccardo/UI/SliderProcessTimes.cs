using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderProcessTimes : MonoBehaviour
{
    public Slider SliderTimeGrill;
    [SerializeField] private float speedTimeGrill = 100f;
    public Text TimeTextGrill;

    private void Update()
    {

        TimerGrill();
    }
   
    public void TimerGrill()
    {
        SliderTimeGrill.value -= Time.deltaTime * speedTimeGrill;
        TimeTextGrill.text = Mathf.Round(SliderTimeGrill.value).ToString();
    }
}


