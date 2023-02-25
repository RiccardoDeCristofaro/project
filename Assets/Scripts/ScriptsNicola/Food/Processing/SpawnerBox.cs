using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBox : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnPoint;
    public RayCast_Test rayCast_Test;
    internal bool enable;
    public BoatPoints boatPoints;

    void Update()
    {
        if (enable)
        {
            rayCast_Test.Spawn(objectToSpawn, spawnPoint);
            boatPoints.BuyItem();
            enable = false;
        }

        rayCast_Test.SwitchLayer(spawnPoint, "LayerReady", "Default");
    }
}
