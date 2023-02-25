using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public Transform spawnPoint;
    public RayCast_Test rayCast_Test;
    internal bool enable;

    void Update()
    {
        if (enable)
        {
            rayCast_Test.Positioning(rayCast_Test.pickObject.transform.gameObject, spawnPoint, true);
            enable = false;
            Debug.Log("Table positioning");
        }

        rayCast_Test.SwitchLayer(spawnPoint, "LayerInteractable", "Default");
    }
}
