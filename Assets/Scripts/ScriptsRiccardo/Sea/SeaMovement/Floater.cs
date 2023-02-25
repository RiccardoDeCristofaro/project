using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;

public class Floater : MonoBehaviour
{
    private Transform seaTransform;
    private Cloth planeCloth;
    [SerializeField]private int vertexIndex = -1;


    private void Start()
    {
        seaTransform = GameObject.Find("Sea").transform;
        planeCloth = seaTransform.GetComponent<Cloth>();
    }
    private void Update()
    {
        GetVertex();
    }
    void GetVertex() // get vertex of the sea and allign them to the boat
    {
        // get the dinstance 
        float distance = 0;
        float closestDistance = 0;
        // look at the vertex of the cloth to floating 
        for(int i= 0; i<planeCloth.vertices.Length; i++)
        {
            if(vertexIndex == -1)
                vertexIndex = i;
            
            distance = Vector3.Distance(planeCloth.vertices[i], transform.position);// calculate the distance between the sea and the boat:
            closestDistance = Vector3.Distance(planeCloth.vertices[vertexIndex], transform.position);

            if(distance < closestDistance) 
            {
                closestDistance = i;
            }
        }
        transform.localPosition = new Vector3
            (
            transform.localPosition.x,
            planeCloth.vertices[vertexIndex].y/10,
            transform.localPosition.z
            );
    }
}
