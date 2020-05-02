using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : Singleton<GameUI>
{
    public enum State
    {
        Normal,

        Building,

        Paused,

        GameOver,

        BuildingWithDrag
    }

    public State state { get; private set; }

    public event Action<State, State> stateChanged;


    public void Pause()
    {
        SetState(State.Paused);
    }

    public void Unpause()
    {
        SetState(State.Normal);
    }

    private void SetState(State newState)
    {
        if (state == newState)
        {
            return;
        }
        State oldState = state;
        if (oldState == State.Paused || oldState == State.GameOver)
        {
            Time.timeScale = 1f;
        }

        switch (newState)
        {
            case State.Normal:
                break;
            case State.Building:
                break;
            case State.BuildingWithDrag:
                break;
            case State.Paused:
            case State.GameOver:
                if (oldState == State.Building)
                {
                   // CancelGhostPlacement();
                }
                Time.timeScale = 0f;
                break;
            default:
                throw new ArgumentOutOfRangeException("newState", newState, null);
        }
        state = newState;
        stateChanged?.Invoke(oldState, state);
    }


}
