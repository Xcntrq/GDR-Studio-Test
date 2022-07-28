using UnityEngine;

public class Player : MonoBehaviour, IKillable
{
    [SerializeField] private GameStateSwitch _gameStateSwitch;
    [SerializeField] private ParticleSystem _particleSystem;

    private IPlayerInput _playerInput;

    public void Kill()
    {
        _playerInput.Disable();
        Instantiate(_particleSystem, transform.position, Quaternion.identity, transform.parent);
        _gameStateSwitch.Lose(1.5f);
        Destroy(gameObject);
    }

    private void Awake()
    {
        if (TryGetComponent(out IPlayerInput playerInput))
        {
            _playerInput = playerInput;
        }
        else
        {
            Debug.LogWarning("Player component couldn't find PlayerInput component");
            _playerInput = new NullPlayerInput();
        }

        _gameStateSwitch.OnGameStateChange += GameStateSwitch_OnGameStateChange;
    }

    private void OnDestroy()
    {
        _gameStateSwitch.OnGameStateChange -= GameStateSwitch_OnGameStateChange;
    }

    private void GameStateSwitch_OnGameStateChange(GameStateSwitch.GameState currentGameState)
    {
        if (currentGameState == GameStateSwitch.GameState.Win)
            _playerInput.Disable();
    }
}
