using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


[DisallowMultipleComponent]
public class PauseClick : MonoBehaviour
{

    public List<GameObject> toActiveObjects = new List<GameObject>();
    private Scene currentScene;
    public KeyCode pauseKey = KeyCode.Escape;
    #region	Pause

    void Update()
    {   
        if (Input.GetKeyDown(pauseKey))
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0; // timer =0
            currentScene = SceneManager.GetActiveScene(); // get the active scene as a variable;
            
            for (int i = 0; i < toActiveObjects.Count; i++)
            {
                toActiveObjects[i].gameObject.SetActive(true);      
            }
            
        }
    }
    public void PlayGame(int scene)
    {
        Time.timeScale = 1; // reset  time
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        for (int i = 0; i < toActiveObjects.Count; i++)
        {
            toActiveObjects[i].gameObject.SetActive(false);
        }
    }

    public void RestartGame(int Scene)
    {
        SceneManager.LoadScene(Scene);
    }
    public void Quit(int scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1;
    }
}
#endregion