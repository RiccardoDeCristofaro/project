using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CannonFire : MonoBehaviour
{
    //[Tooltip("cannon ammo")]
    //public float capacity = 0;
    [Tooltip("throw force: ")]
    [SerializeField] public float throwForce;

    [SerializeField] KeyCode fireButton;

    public ShakeCam shakeCam_Script;
    private Rigidbody recipeRigidobdy;
    public ChangeCam change_Cam_script;

    void Update()
    {
        if (Input.GetKeyDown(fireButton) && transform.childCount == 1)
        {
            recipeRigidobdy = transform.GetChild(0).GetComponent<Rigidbody>();
            transform.GetChild(0).SetParent(null);            
            recipeRigidobdy.isKinematic = false;

            // adding force to thrown out the food
            recipeRigidobdy.AddForce(transform.right * throwForce, ForceMode.Impulse);
            StartCoroutine(shakeCam_Script.Shake());
            StartCoroutine(FireReset());
            transform.parent.gameObject.layer = LayerMask.NameToLayer("LayerInteractable");


        }
    }

    IEnumerator FireReset()
    {
        Debug.Log("you have fired");
        yield return new WaitForSeconds(3);
        Debug.Log("you have waited 3 seconds");
        change_Cam_script.ExitCannonCam();
        yield return null;
    }
}
