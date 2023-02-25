using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHP : MonoBehaviour
{

    // healthpoints
    public Slider sliderHP;
    public float damage = 0.35f;
    public float hp;

    public void HealthPoint()
    {
        // hp
        sliderHP.value -= Mathf.Clamp(damage, 0, 1);
    }
}
