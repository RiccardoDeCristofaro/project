using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCam : MonoBehaviour
{
    [SerializeField, Range(-1f, 1f)] private float xMax;
	[SerializeField, Range(-1f, 1f)] private float XMin;
	[SerializeField, Range(-1f, 1f)] private float yMax;
	[SerializeField, Range(-1f, 1f)] private float yMin;
    public float magnitude;
    public float duration;
    public IEnumerator Shake() // magnitude : the strenght of our shake;
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f; 

        while (elapsed < duration) 
        {
            float x = Random.Range(XMin, xMax) * magnitude; // the strenght of the shake by axis
            float y = Random.Range(yMin, yMax) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z); // shake changed transform

            elapsed += Time.deltaTime; // timer

            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
