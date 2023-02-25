
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public float height;
    public float time;


    private void Start()
    {
        iTween.MoveBy(this.gameObject, iTween.Hash(
            "y",
            height,
            "time",
            time, 
            "loopType",
            "pingpong",
            "easytype",
            iTween.EaseType.easeInOutSine)
            );
    }
}
