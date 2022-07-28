using System;
using System.Collections;
using UnityEngine;

public class GameStateSwitch : MonoBehaviour
{
    private GameState _currentGameState;

    public event Action<GameState> OnGameStateChange;

    public enum GameState
    {
        Playing,
        Win,
        Loss,
    }

    public GameState CurrentGameState => _currentGameState;

    public void Win()
    {
        StartCoroutine(SetState(GameState.Win, 0f, 0f));
    }

    public void Lose(float delay)
    {
        StartCoroutine(SetState(GameState.Loss, delay, 0f));
    }

    private void Start()
    {
        Play();
    }

    private void Play()
    {
        StartCoroutine(SetState(GameState.Playing, 0f, 1f));
    }

    private IEnumerator SetState(GameState gameState, float delay, float timeScale)
    {
        yield return new WaitForSeconds(delay);

        _currentGameState = gameState;
        OnGameStateChange?.Invoke(_currentGameState);
        Time.timeScale = timeScale;
    }
}
