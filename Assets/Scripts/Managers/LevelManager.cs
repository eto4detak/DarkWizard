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

    public float timer;
    public Spawner spawner;
    public LevelData levelData;
    public LevelState levelState { get; protected set; }

    public event Action levelCompleted;
    public event Action levelFailed;

    public void StartBattle()
    {
        ChangeLevelState(LevelState.StartBattle);
        StartCoroutine(StartTimer());
        TotalStatisticUI.instance.StartTimer();
    }

    public IEnumerator StartTimer()
    {
        while (true)
        {
            if(levelState == LevelState.StartBattle)  timer += Time.deltaTime;
            yield return null;
        }
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
                spawner.StartSpawn();
                break;
            case LevelState.AllEnemiesSpawned:
                break;
            case LevelState.Lose:
                StartCoroutine(GameOver());
                spawner.StopSpawn();
                break;
            case LevelState.Win:
                StartCoroutine(GameComplete());
                break;
        }
    }



    private void CreateMags()
    {
        Unit hero = Instantiate(UnitManager.instance.unitPrefab);
        UnitManager.instance.AttachedUnit(hero);
        hero.dieUnit.AddListener(StartGameOver);

        hero.name = "Player1";
        hero.enabled = false;
        hero.transform.position = Vector3.zero;
        hero.gameObject.AddComponent<KeyController>().enabled = false;
        hero.transform.position = new Vector3(-10, 0, 0);
        UnitManager.instance.hero = hero;
        SpellBookUI.instance.Setup(hero);

        Unit enemy = Instantiate(UnitManager.instance.unitPrefab);
        enemy.name = "AI";
        enemy.enabled = false;
        enemy.transform.position = new Vector3(10, 0, 0);
        enemy.gameObject.AddComponent<AIMag>().enabled = false;
        enemy.target = hero.gameObject;
        hero.target = enemy.gameObject;

        FollowingCamera camera = Camera.main.GetComponent<FollowingCamera>();
        camera.target = hero.gameObject;
    }


    public IEnumerator GameOver()
    {
        ChangeLevelState(LevelState.Lose);

        UnitManager.instance.SetVitoryEnemies();
        float time = 2f;
        yield return new WaitForSeconds(time);
        levelFailed?.Invoke();
        EndGameScreen.instance.ShowPanel();
    }

    public IEnumerator GameComplete()
    {
        ChangeLevelState(LevelState.Win);

        float time = 2f;
        yield return new WaitForSeconds(time);
        levelCompleted?.Invoke();
    }


    public void StartGameOver()
    {
        StartCoroutine(GameOver());
    }


}

public class LevelData
{
    public int sceneNumber;
    public int levelNumber;
    public int partyCount;
    public int roundCount;
}
