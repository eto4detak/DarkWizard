using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameScreen : Singleton<EndGameScreen>
{
    public string levelCompleteText = "{0} COMPLETE!";
    public string levelFailedText = "{0} FAILED!";
    public Text endGameMessageText;
    public Image background;
    public Color winBackgroundColor;
    public Color loseBackgroundColor;
    public Canvas endGameCanvas;
    public Canvas nextLevelButton;

    protected override void OnDestroy()
    {
        base.OnDestroy();
        SafelyUnsubscribe();
    }

    public void RestartLevel()
    {
        SafelyUnsubscribe();
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }


    protected void Victory()
    {
        ShowPanel();
        nextLevelButton.enabled = true;
        nextLevelButton.gameObject.SetActive(true);
        background.color = winBackgroundColor;
    }

    protected void Defeat( )
    {
        ShowPanel();
        if (nextLevelButton != null)
        {
            nextLevelButton.enabled = false;
            nextLevelButton.gameObject.SetActive(false);
        }
        background.color = loseBackgroundColor;
    }

    public void ShowPanel()
    {
        endGameCanvas.enabled = true;
    }

    protected void SafelyUnsubscribe()
    {
        if (LevelManager.instanceExists)
        {
            LevelManager.instance.levelCompleted -= Victory;
            LevelManager.instance.levelFailed -= Defeat;

        }
    }


}
