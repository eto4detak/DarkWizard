using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float startDelay = 1f;
    public float endDelay = 3f;
    public TextMeshProUGUI messageText;
    public bool startGame;
    public bool restartGame;
    public bool continueGame;

    private int levelNumber = 0;

    #region Singleton
    static protected GameManager s_Instance;
    static public GameManager instance { get { return s_Instance; } }
    #endregion

    private void Awake()
    {
        #region Singleton
        if (s_Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        s_Instance = this;
        DontDestroyOnLoad(gameObject);
        #endregion
    }

    void Start()
    {
        SaveLoad.GetInstance().Load();

    }

    public void StartLevel()
    {

    }

    private void ContinueGame()
    {
        UnitManager.instance.StartUnits();
    }

}