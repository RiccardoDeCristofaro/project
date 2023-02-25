using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class GameOver : MonoBehaviour
{
    public Text pointsChecker;
    public Slider hpChecker;
    public GameObject objectScreen;// game over screen object
    public Slider gameTime;
    public List<Texture> screensWin = new List<Texture>();
    public Texture loseScreen;
    public RawImage gameScreen;  // robo bianco
    private bool endedGame = true ;
    public MovePlayer playerMovement;
    public RayCast_Test rayCast_Test;

    private void Start()
    {
        
        gameScreen = objectScreen.GetComponent<RawImage>();
    }
    private void Update()
    {
        if (gameTime.value == 0 || pointsChecker.text == "0")
        {
            objectScreen.SetActive(true);
            playerMovement.enabled = false;
            Cursor.lockState = CursorLockMode.Confined;
            rayCast_Test.enabled = false;
        }
                
    }
}
