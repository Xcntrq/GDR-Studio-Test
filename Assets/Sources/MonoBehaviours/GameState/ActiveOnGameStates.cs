using System.Collections.Generic;
using UnityEngine;

public class ActiveOnGameStates : MonoBehaviour
{
    [SerializeField] private GameStateSwitch _gameStateSwitch;
    [SerializeField] private List<GameStateSwitch.GameState> _gameStates;

    private void Awake()
    {
        _gameStateSwitch.OnGameStateChange += GameStateSwitch_OnGameStateChange;
    }

    private void Start()
    {
        GameStateSwitch_OnGameStateChange(_gameStateSwitch.CurrentGameState);
    }

    private void OnDestroy()
    {
        _gameStateSwitch.OnGameStateChange -= GameStateSwitch_OnGameStateChange;
    }

    private void GameStateSwitch_OnGameStateChange(GameStateSwitch.GameState currentGameState)
    {
        bool isActive = false;
        foreach (var gameState in _gameStates)
        {
            if (gameState != currentGameState)
                continue;

            isActive = true;
            break;
        }

        gameObject.SetActive(isActive);
    }
}
