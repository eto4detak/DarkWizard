using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameUIState = GameUI.State;


public class PauseMenu : MonoBehaviour
{
    protected enum State
    {
        Open,
        LevelSelectPressed,
        RestartPressed,
        Closed
    }

    public Canvas pauseMenuCanvas;

    public Text titleText;

    public Text descriptionText;

    public Button levelSelectConfirmButton;

    public Button restartConfirmButton;

    public Button levelSelectButton;

    public Button restartButton;

    public Image topPanel;

    public Color topPanelDisabledColor = new Color(1, 1, 1, 1);

    protected State m_State;

    bool m_MenuChangedThisFrame;

    protected void Start()
    {

        if (GameUI.instanceExists)
        {
            GameUI.instance.stateChanged += OnGameUIStateChanged;
        }
    }

    public void OpenPauseMenu()
    {
        SetPauseMenuCanvas(true);

        if (titleText != null)
        {
            titleText.text = SceneManager.GetActiveScene().name;
        }

        m_State = State.Open;
    }

    protected void OnGameUIStateChanged(GameUIState oldState, GameUIState newState)
    {
        m_MenuChangedThisFrame = true;
        if (newState == GameUIState.Paused)
        {
            OpenPauseMenu();
        }
        else
        {
            ClosePauseMenu();
        }
    }

    public void LevelSelectPressed()
    {
        bool open = m_State == State.Open;
        restartButton.interactable = !open;
        topPanel.color = open ? topPanelDisabledColor : Color.white;
        levelSelectConfirmButton.gameObject.SetActive(open);
        m_State = open ? State.LevelSelectPressed : State.Open;
    }

    public void RestartPressed()
    {
        bool open = m_State == State.Open;
        levelSelectButton.interactable = !open;
        topPanel.color = open ? topPanelDisabledColor : Color.white;
        restartConfirmButton.gameObject.SetActive(open);
        m_State = open ? State.RestartPressed : State.Open;
    }

    public void RestartConfirmPressed()
    {
        RestartCurrentScene();
    }

    public void RestartCurrentScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

    public void ClosePauseMenu()
    {
        SetPauseMenuCanvas(false);

        levelSelectConfirmButton.gameObject.SetActive(false);
        restartConfirmButton.gameObject.SetActive(false);
        levelSelectButton.interactable = true;
        restartButton.interactable = true;
        topPanel.color = Color.white;

        m_State = State.Closed;
    }

    protected void Awake()
    {
        SetPauseMenuCanvas(false);
        m_State = State.Closed;
    }


    protected virtual void Update()
    {
        if (m_MenuChangedThisFrame)
        {
            m_MenuChangedThisFrame = false;
            return;
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) && GameUI.instance.state == GameUIState.Paused)
        {
            Unpause();
        }
    }

    protected void SetPauseMenuCanvas(bool enable)
    {
        pauseMenuCanvas.enabled = enable;
    }

    public void Pause()
    {
        if (GameUI.instanceExists)
        {
            GameUI.instance.Pause();
        }
    }

    public void Unpause()
    {
        if (GameUI.instanceExists)
        {
            GameUI.instance.Unpause();
        }
    }
}