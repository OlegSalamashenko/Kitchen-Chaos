using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitcheGameManager : MonoBehaviour
{
    public static KitcheGameManager Instance { get; private set; }


    public event EventHandler OnStateChange;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;


    private enum State { 
     WaitingToStart,
     CountdownToStart,
     GamePlaying,
     GameOver,    
    }

    private State state;
    private float countdownToStartTimer = 3f;
    private float gamePlayingToStartTimer ;
    private float gamePlayingToStartTimerMax = 30f;
    private bool isGamePause = false;

    private void Awake()
    {
        state = State.WaitingToStart;
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(state ==State.WaitingToStart)
        {
            state = State.CountdownToStart;
            OnStateChange?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                break;


            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingToStartTimer = gamePlayingToStartTimerMax;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;


            case State.GamePlaying:
                gamePlayingToStartTimer -= Time.deltaTime;
                if (gamePlayingToStartTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;


            case State.GameOver:
                break;


        }
        Debug.Log(state);
    }


    public bool IsGamePlaying() { 
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownTostartTimer() { 
        return countdownToStartTimer;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetPlayingTimerNormilized()
    {
        return 1 - (gamePlayingToStartTimer / gamePlayingToStartTimerMax);
    }

    public void TogglePauseGame()
    {
        isGamePause = !isGamePause;
        if (isGamePause)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this,EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
        
    }


}
