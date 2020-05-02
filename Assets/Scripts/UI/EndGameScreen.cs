using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameScreen : MonoBehaviour
{
    public string levelCompleteText = "{0} COMPLETE!";
    public string levelFailedText = "{0} FAILED!";
    public Text endGameMessageText;
    public Image background;
    public Color winBackgroundColor;
    public Color loseBackgroundColor;
    public Canvas endGameCanvas;
    public Canvas nextLevelButton;

    protected void Start()
    {
        LevelManager.instance.levelCompleted += Victory;
        LevelManager.instance.levelFailed += Defeat;
    }

    protected void OnDestroy()
    {
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
        //if ((victorySound != null) && (audioSource != null))
        //{
        //    audioSource.PlayOneShot(victorySound);
        //}

        //first check if there are any more levels after this one
        //if (nextLevelButton == null || !GameManager.instanceExists)
        //{
        //    return;
        //}
        //GameManager gm = GameManager.instance;
        //LevelItem item = gm.GetLevelForCurrentScene();
        //LevelList list = gm.levelList;
        //int levelCount = list.Count;
        //int index = -1;
        //for (int i = 0; i < levelCount; i++)
        //{
        //    if (item == list[i])
        //    {
        //        index = i;
        //        break;
        //    }
        //}
        //if (index < 0 || index == levelCount - 1)
        //{
        //    nextLevelButton.enabled = false;
        //    nextLevelButton.gameObject.SetActive(false);
        //    return;
        //}
        OpenEndGameScreen(levelCompleteText);
        nextLevelButton.enabled = true;
        nextLevelButton.gameObject.SetActive(true);
        background.color = winBackgroundColor;
    }

    protected void Defeat( )
    {

        //if ((defeatSound != null) && (audioSource != null))
        //{
        //    audioSource.PlayOneShot(defeatSound);
        //}
        OpenEndGameScreen(levelFailedText);
        if (nextLevelButton != null)
        {
            nextLevelButton.enabled = false;
            nextLevelButton.gameObject.SetActive(false);
        }
        background.color = loseBackgroundColor;
    }

    protected void OpenEndGameScreen(string endResultText)
    {
         int level = LevelManager.instance.GetCurrentLevel();
        endGameCanvas.enabled = true;
        endGameMessageText.text = string.Format(endResultText, level);
        //  int score = CalculateFinalScore();
        // scorePanel.SetStars(score);
        //if (level != null)
        //{
        //    endGameMessageText.text = string.Format(endResultText, level.name.ToUpper());
        //    GameManager.instance.CompleteLevel(level.id, score);
        //}
        //else
        //{
        //    // If the level is not in LevelList, we should just use the name of the scene. This will not store the level's score.
        //    string levelName = SceneManager.GetActiveScene().name;
        //    endGameMessageText.text = string.Format(endResultText, levelName.ToUpper());
        //}


        //if (!HUD.GameUI.instanceExists)
        //{
        //    return;
        //}
        //if (HUD.GameUI.instance.state == HUD.GameUI.State.Building)
        //{
        //    HUD.GameUI.instance.CancelGhostPlacement();
        //}
        // GameUI.instance.GameOver();
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
