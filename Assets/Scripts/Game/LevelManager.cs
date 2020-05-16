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
        StartLevel,
        Building,
        StartBattle,
        AllEnemiesSpawned,
        Lose,
        Win
    }
    public LevelData levelData;
    public LevelState levelState { get; protected set; }

    public event Action levelCompleted;
    public event Action levelFailed;

    public void StartBattle()
    {
        ChangeLevelState(LevelState.StartBattle);
       // UnitManager.instance.hero.dieUnit.AddListener(GameOver);
    }
    public void StartLevel()
    {
        ChangeLevelState(LevelState.StartLevel);
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
        if (levelState == newState)
        {
            return;
        }

        LevelState oldState = levelState;
        levelState = newState;
        switch (newState)
        {
            case LevelState.StartLevel:
                CreateMags();
                break;
            case LevelState.StartBattle:
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


    private void CreateMags()
    {
        Unit hero = Instantiate(UnitManager.instance.unitPrefab);
        Unit enemy = Instantiate(UnitManager.instance.unitPrefab);

        hero.name = "Player1";
        hero.enabled = false;
        hero.transform.position = new Vector3(-10, 0, 0);
        hero.gameObject.AddComponent<KeyController>().enabled = false;
        hero.target = enemy;
        UnitManager.instance.hero = hero;
        SpellBookUI.instance.Setup(hero);

        enemy.name = "AI";
        enemy.enabled = false;
        enemy.transform.position = new Vector3(10, 0, 0);
        enemy.gameObject.AddComponent<AIMag>().enabled = false;
        enemy.target = hero;

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
