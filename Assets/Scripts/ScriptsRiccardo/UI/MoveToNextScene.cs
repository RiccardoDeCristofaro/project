using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class MoveToNextScene : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject menu;
    #region	Public variables
    #endregion
    #region	Private variables
    #endregion
    #region	Lifecycle

    #endregion
    #region	Public methods
    // load scene of index "sceneId"
    public void NewGameButton(int SceneId)
    {
        SceneManager.LoadScene(SceneId);
    }

    public void TutorialButton()
    {
        tutorialPanel.SetActive(true);
        menu.SetActive(false);
    }

    public void Quit()
    {
        // exit app;
        Application.Quit();
    }

    public void CloseTutorial()
    {
        tutorialPanel?.SetActive(false);
        menu.SetActive(true);
    }

    #endregion
    #region	Private methods
    #endregion
}
