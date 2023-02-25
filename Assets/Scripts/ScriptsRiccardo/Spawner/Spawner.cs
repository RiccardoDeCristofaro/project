using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Camera cam;
    public GameObject foodPrefab;
    public GameObject spawnPoint, playerRotationPoint;
    [SerializeField]
    public float rotX, rotY, rotZ;
    private bool recentlySpawned = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !recentlySpawned)
        {
            recentlySpawned = true;
            CheckView();
            StartCoroutine(SpawnDelayer());
        }
    }
    private void CheckView()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 5f /*distance*/))
        {
            if (hit.transform.tag == "spawner")
            {
                if (hit.transform.name == "Crate")
                {
                    rotX = 0f;
                    rotY = 270f;
                    rotZ = 0f;
                    ViewObject(foodPrefab, rotX, rotY, rotZ, ray);
                }
                if (hit.transform.name == "Crate_1")
                {

                }
            }
            
        }
    }
    private void ViewObject(GameObject prefab, float rotX, float rotY, float rotZ, Ray ray)
    {
        Instantiate(prefab, new Vector3(
            spawnPoint.transform.position.x,
            ray.origin.y,
            spawnPoint.transform.position.z),
            Quaternion.Euler(rotX, playerRotationPoint.transform.localRotation.eulerAngles.y + rotY, rotZ)
            );
    }
    IEnumerator SpawnDelayer() // delay 3 second couroutine
    {
        yield return new WaitForSeconds(1);
        recentlySpawned = false;
    }
}
