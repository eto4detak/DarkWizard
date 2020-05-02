using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public enum LevelState
    {
        Intro,
        Building,
        SpawningEnemies,
        AllEnemiesSpawned,
        Lose,
        Win
    }
    public LevelData levelData;
    public LevelState levelState { get; protected set; }

    public event Action levelCompleted;
    public event Action levelFailed;

    public void StartLevel()
    {
        UnitManager.instance.hero.dieUnit.AddListener(GameOver);
    }


    public int  GetCurrentLevel()
    {
        return SceneManager.sceneCountInBuildSettings;
    }


    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene("Level"+ levelNumber);
    }
    public void LoadNextLevel()
    {
        LoadLevel(levelData.levelNumber+1);
    }

    public void RestartLevel()
    {
        if(levelData != null)
         LoadLevel(levelData.levelNumber);
    }

    private void ChangeLevelState(LevelState newState)
    {
        if (levelState == newState || levelState == LevelState.Win)
        {
            return;
        }

        LevelState oldState = levelState;
        levelState = newState;
        switch (newState)
        {
            case LevelState.SpawningEnemies:
                UnitManager.instance.StartUnits();
                break;
            case LevelState.AllEnemiesSpawned:
                break;
            case LevelState.Lose:
                UnitManager.instance.SetVitoryEnemies();
                StartCoroutine(SendAboutEndGame());
                break;
            case LevelState.Win:
                StartCoroutine(SendLevelCompleted());
                break;
        }
    }

    public IEnumerator SendAboutEndGame()
    {
        float time = 2f;
        yield return new WaitForSeconds(time);
        levelFailed?.Invoke();
    }

    public IEnumerator SendLevelCompleted()
    {
        float time = 2f;
        yield return new WaitForSeconds(time);
        levelCompleted?.Invoke();
    }

    private void GameOver()
    {
        ChangeLevelState(LevelState.Lose);
    }
    private void GameComplete()
    {
        ChangeLevelState(LevelState.Win);
    }

}

public class LevelData
{
    public int sceneNumber;
    public int levelNumber;
    public int partyCount;
    public int roundCount;
}
