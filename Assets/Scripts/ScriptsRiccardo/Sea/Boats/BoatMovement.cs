using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public Transform Target;

    public float RotationSpeed;

    public float CircleRadius = 1;

    public float ElevationOffset = 0;

    private Vector3 positionOffset;
    private float angle;

    private void Start()
    {
        RotationSpeed = Random.Range(1.3f, 3.6f);
    }
    private void LateUpdate()
    {
        positionOffset.Set(
            Mathf.Cos(angle) * CircleRadius,
            ElevationOffset,
            Mathf.Sin(angle) * CircleRadius
        );
        transform.position = Target.position + positionOffset;
        angle +=  RotationSpeed * Time.deltaTime;
    }
}
