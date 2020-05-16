using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float startDelay = 1f;
    public float endDelay = 3f;
    public TextMeshProUGUI messageText;
    public bool startGame;
    public bool restartGame;
    public bool continueGame;

    private int levelNumber = 0;

    private void Start()
    {
        SaveLoad.GetInstance().Load();
        LevelManager.instance.StartLevel();
    }

    public void StartLevel()
    {

    }

    private void ContinueGame()
    {
        UnitManager.instance.StartUnits();
    }

}